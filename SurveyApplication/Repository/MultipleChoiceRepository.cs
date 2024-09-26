using Dapper;
using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class MultipleChoiceRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : IMultipleChoiceRepository
{
    #region Fields

    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
    private readonly IGarnetClient _garnetClient = garnetClient;

    private static readonly string checkQuestionQuery = """
                SELECT COUNT(1)
                FROM question
                WHERE question_id = :questionId
                AND question_type_id = 2;
                """;

    private static readonly string checkMatchedAnswerQuery = """
                SELECT COUNT(1)
                FROM multiple_choice_questions
                WHERE question_id = :questionId
                """;

    private static readonly string setMaxAnswerCommand = """
                     INSERT INTO multiple_choice_questions (max_choice_amount,question_id)
                     VALUES (:maxSize,:questionId)
                     RETURNING choice_id;
                     """;

    private static readonly string getMaxAnswerQuery = """
                     SELECT COUNT(1)
                     FROM multiple_choice_questions
                     WHERE question_id = :questionId
                     """;

    private static readonly string getAnswersQuery = """
                     SELECT answer
                     FROM multiple_choice_answers
                     WHERE question_id = :questionId
                     """;

    private static readonly string addChoiceCommand = """
                                INSERT INTO other_choices (choice,multiple_choice_questions_id) 
                                VALUES (:choice,:multipleChoiceQuestionsId)
                                """;

    private static readonly string getChoiceIdQuery = """
                                 SELECT choice_id
                                 FROM multiple_choice_questions
                                 WHERE question_id = :questionId
                                 """;

    private static readonly string getChoicesQuery = """
                                SELECT choice
                                FROM other_choices
                                WHERE multiple_choice_questions_id = :choiceId
                                """;

    private static readonly string getQuestionIdQuery = """
                             SELECT question_id
                             FROM multiple_choice_questions
                             WHERE choice_id = :multiple_Choice_Question_Id
                             """;

    private static readonly string getSurveyIdQuery = """
                             SELECT survey_id
                             FROM question
                             WHERE question_id = :questionId
                             """;

    private static readonly string saveAnswerCommand = """
                               INSERT INTO multiple_choice_answers (answer,question_id) 
                               VALUES (:answer,:questionId)
                               """;

    private static readonly string checkMaxAnswerQuery = """
                                     SELECT MAX(max_choice_amount) AS max_choice_amount
                                     FROM multiple_choice_questions
                                     WHERE question_id = :questionId
                                     """;

    private static readonly string checkAnswerQuery = """
                                  SELECT COUNT(1)
                                  FROM multiple_choice_answers
                                  WHERE question_id = :questionId
                                  """;

    private static readonly string createChoicesViewQuery = """
                                 SELECT EXISTS (
                                     SELECT 1
                                     FROM other_choices oc
                                     JOIN multiple_choice_questions mcq ON oc.multiple_choice_questions_id = mcq.choice_id
                                     WHERE oc.choice = :answer
                                       AND mcq.question_id = :questionId
                                 )
                                 """;

    private static readonly string checkExistChoiceQuery = """
                                 SELECT COUNT(1)
                                 FROM other_choices
                                 WHERE choice = :choice
                                 AND multiple_choice_questions_id = :multipleChoiceQuestionsId
                                 """;



    #endregion

    public async Task<int> SetMaxAnswerAmount(MCSetMaxAnswerAmountDto question)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        /////
        int matchedQuestionCount = await connection.ExecuteScalarAsync<int>(checkQuestionQuery, new { questionId = question.Question_Id });
        int matchedAnswerCount = await connection.ExecuteScalarAsync<int>(checkMatchedAnswerQuery, new { questionId = question.Question_Id });

        if (matchedQuestionCount < 1) return -1;
        if (matchedAnswerCount > 0) return -1;
        /////

        var parameters = new
        {
            maxSize = question.Max_Choice_Amount,
            questionId = question.Question_Id
        };

        return await connection.ExecuteScalarAsync<int>(setMaxAnswerCommand, parameters);
    }

    public async Task AddChoice(MCChoicesDto newChoice)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var parameters = new
        {
            choice = newChoice.Choice,
            multipleChoiceQuestionsId = newChoice.Multiple_Choice_Question_Id
        };

        await connection.ExecuteAsync(addChoiceCommand, parameters);

        int questionId = await connection.ExecuteScalarAsync<int>(getQuestionIdQuery, new { newChoice.Multiple_Choice_Question_Id });

        int surveyId = await connection.ExecuteScalarAsync<int>(getSurveyIdQuery, new { questionId });

        await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{surveyId}");
        await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}");
    }

    public async Task SaveAnswer(MCAnswersDto answerModel)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var parameters = new
        {
            answer = answerModel.Answer,
            questionId = answerModel.Question_Id
        };

        int surveyId = await connection.ExecuteScalarAsync<int>(getSurveyIdQuery, new { questionId = answerModel.Question_Id });

        await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{surveyId}");
        await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}");

        await connection.ExecuteAsync(saveAnswerCommand, parameters);
    }



    public async Task<bool> SetMaxAmountExist(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        return await connection.ExecuteScalarAsync<bool>(getMaxAnswerQuery, new { questionId });

    }

    public async Task<bool> ChoiceExist(int multipleChoiceQuestionsId, string choice)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var parameters = new
        {
            choice,
            multipleChoiceQuestionsId
        };

        return await connection.ExecuteScalarAsync<bool>(checkExistChoiceQuery, parameters);
    }

    public async Task<bool> AnswerExist(int questionId, string answer)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var existAnswers = await connection.QueryAsync<string>(getAnswersQuery, new { questionId });

        return existAnswers.Any(existAnswer => existAnswer.Equals(answer));
    }

    public async Task<bool> AnswerIsAnChoice(int questionId, string answer)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int choiceId = await connection.ExecuteScalarAsync<int>(getChoiceIdQuery, new { questionId });

        var existChoices = await connection.QueryAsync<string>(getChoicesQuery, new { choiceId });

        return existChoices.Any(existChoice => existChoice.Equals(answer));
    }

    public async Task<bool> IsAnswerAmountWithinLimit(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int matchedAnswerCount = await connection.ExecuteScalarAsync<int>(checkAnswerQuery, new { questionId });
        int maxAnswerAmount = await connection.ExecuteScalarAsync<int>(checkMaxAnswerQuery, new { questionId });

        return matchedAnswerCount < maxAnswerAmount;

    }
}

