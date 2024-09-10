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
                              VALUES (@firstChoice,@secondChoice,@questionId)
                              """;

    private static readonly string surveyIdQuery = """
                              SELECT survey_id
                              FROM question
                              WHERE question_id = @questionId
                              """;

    private static readonly string checkQuestionQuery = """ 
                                    SELECT COUNT(1)
                                    FROM question 
                                    WHERE question_id = @questionId     
                                        AND survey_id = @surveyId
                                    """;

    private static readonly string getChoicesQuery = """
                                 SELECT first_choice, second_choice 
                                 FROM single_choice_questions 
                                 WHERE question_id = @questionId
                                 """;

    private static readonly string checkAnswerQuery = """
                            SELECT COUNT(1)
                            FROM single_choice_answers 
                            WHERE question_id = @questionId 
                                AND survey_id = @surveyId
                            """;

    private static readonly string insertAnswerCommand = """
                            INSERT INTO single_choice_answers (answer,question_id,survey_id) 
                            VALUES (@answer,@questionId,@surveyId)
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

        int existingQuestionCount = await connection.ExecuteScalarAsync<int>(checkQuestionQuery,
            new
            {
                questionId = answer.Question_Id,
                surveyId = answer.Survey_Id
            });

        if (existingQuestionCount < 1) throw new ArgumentException("No such question was found");

        var validChoices = connection.Query(getChoicesQuery, new { questionId = answer.Question_Id })
                                 .SelectMany(row => new List<string> { row.first_choice, row.second_choice })
                                 .ToList();

        string userAnswer = answer.Answer;

        if (!validChoices.Contains(userAnswer)) throw new ArgumentException("Answer not found in the choices");

        int existingCount = await connection.ExecuteScalarAsync<int>(checkAnswerQuery,
            new
            {
                questionId = answer.Question_Id,
                surveyId = answer.Survey_Id
            });

        if (existingCount > 0) throw new ArgumentException("You replied this question before");

        var parameters = new
        {
            answer = answer.Answer,
            questionId = answer.Question_Id,
            surveyId = answer.Survey_Id,
        };

        await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{answer.Survey_Id}");
        await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{answer.Survey_Id}");

        await connection.ExecuteAsync(insertAnswerCommand, parameters);
    }

    public Task<bool> QuestionExist(int questionId, string firstChoice, string secondChoice)
    {
        throw new NotImplementedException();
    }
}
