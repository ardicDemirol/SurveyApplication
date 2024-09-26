using Dapper;
using SurveyApplication.Dtos.SingleChoiceDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class SingleChoiceRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : ISingleChoiceRepository
{
    #region Fields

    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
    private readonly IGarnetClient _garnetClient = garnetClient;

    private static readonly string insertChoiceCommand = """
                              INSERT INTO single_choice_questions (first_choice,second_choice,question_id) 
                              VALUES (:firstChoice,:secondChoice,:questionId)
                              """;

    private static readonly string insertAnswerCommand = """
                            INSERT INTO single_choice_answers (answer, question_id, survey_id )
                            VALUES (:answer, :questionId, :surveyId)
                            """;

    private static readonly string surveyIdQuery = """
                              SELECT survey_id
                              FROM question
                              WHERE question_id = :questionId
                              """;

    private static readonly string questionExistQuery = """ 
                                    SELECT COUNT(1)
                                    FROM question 
                                    WHERE question_id = :questionId     
                                    """;

    private static readonly string answerExistQuery = """
                            SELECT COUNT(1)
                            FROM single_choice_answers 
                            WHERE question_id = :questionId 
                            """;

    private static readonly string choicesExistQuery = """
                            SELECT COUNT(1)
                            FROM single_choice_questions
                            WHERE question_id = :questionId
                            """;

    private static readonly string getQuestionTypeQuery = """
                            SELECT question_type_id
                            FROM question
                            WHERE question_id = :questionId
                            """;

    private static readonly string getChoicesQuery = """
                            SELECT first_choice, second_choice
                            FROM single_choice_questions
                            WHERE question_id = :questionId
                            """;


    #endregion

    public async Task AddChoice(SCQuestionDto singleChoice)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int surveyId = await connection.ExecuteScalarAsync<int>(surveyIdQuery, new { questionId = singleChoice.Question_Id });

        var parameters = new
        {
            firstChoice = singleChoice.First_Choice,
            secondChoice = singleChoice.Second_Choice,
            questionId = singleChoice.Question_Id
        };

        await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{surveyId}");
        await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}");

        await connection.ExecuteAsync(insertChoiceCommand, parameters);
    }

    public async Task SaveAnswer(SCAnswerDto answer)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int surveyId = await connection.ExecuteScalarAsync<int>(surveyIdQuery, new { questionId = answer.Question_Id });

        var parameters = new
        {
            answer = answer.Answer,
            questionId = answer.Question_Id,
            surveyId,
        };

        await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{answer.Survey_Id}");
        await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{answer.Survey_Id}");

        await connection.ExecuteAsync(insertAnswerCommand, parameters);
    }


    public async Task<bool> QuestionExist(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int existingQuestionCount = await connection.ExecuteScalarAsync<int>(questionExistQuery, new { questionId });

        return existingQuestionCount > 0;
    }

    public async Task<bool> QuestionTypeIsCorrect(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int questionTypeId = await connection.ExecuteScalarAsync<int>(getQuestionTypeQuery, new { questionId });

        return questionTypeId == 1;
    }

    public async Task<bool> ChoicesAreEquals(string firstChoice, string secondChoice)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        return firstChoice.Equals(secondChoice, StringComparison.CurrentCultureIgnoreCase);
    }

    public async Task<bool> ChoicesExist(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int existingChoicesCount = await connection.ExecuteScalarAsync<int>(choicesExistQuery, new { questionId });

        return existingChoicesCount > 0;
    }

    public async Task<bool> AnswerIsAnChoice(int questionId, string answer)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var choices = await connection.QueryAsync<string>(getChoicesQuery, new { questionId });

        return choices.Contains(answer);

    }

    public async Task<bool> AnswerExist(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int existingCount = await connection.ExecuteScalarAsync<int>(answerExistQuery, new { questionId });

        return existingCount > 0;
    }

}
