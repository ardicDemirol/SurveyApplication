using Dapper;
using SurveyApplication.Dtos.TextBasedDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class TextBasedRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : ITextBasedRepository
{
    #region Variables
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
    private readonly IGarnetClient _garnetClient = garnetClient;

    static readonly string answerCommand = """
                              INSERT INTO text_based_answer (answer,question_id) 
                              VALUES (:answer,:questionId)
                              """;

    static readonly string setRelationCommand = """
                              INSERT INTO text_based_question_type_relationship (text_type_id,question_id) 
                              VALUES (:textTypeId,:questionId)
                              """;

    static readonly string getTextTypeIdQuery = """
                              SELECT text_type_id
                              FROM text_based_question_type_relationship
                              WHERE question_id = :questionId
                              """;

    static readonly string surveyIdQuery = """
                              SELECT survey_id
                              FROM question
                              WHERE question_id = :questionId
                              """;

    static readonly string questionExistQuery = """ 
                              SELECT COUNT(1)
                              FROM question 
                              WHERE question_id = :questionId     
                              """;

    private static readonly string getQuestionTypeQuery = """
                            SELECT question_type_id
                            FROM question
                            WHERE question_id = :questionId
                            """;

    private static readonly string answerExistQuery = """
                            SELECT COUNT(1)
                            FROM text_based_answer
                            WHERE question_id = :questionId 
                            """;

    private static readonly string relationExistQuery = """
                            SELECT COUNT(1)
                            FROM text_based_question_type_relationship
                            WHERE question_id = :questionId
                            """;


    #endregion

    public async Task SetRelation(TBQSetRelationshipTypeDto relation)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var parameters = new
        {
            textTypeId = relation.Text_Type_Id,
            questionId = relation.Question_Id
        };

        await connection.ExecuteScalarAsync(setRelationCommand, parameters);
    }

    public async Task SaveAnswer(TBQAnswersDto answer)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int surveyId = await connection.ExecuteScalarAsync<int>(surveyIdQuery, new { questionId = answer.Question_Id });

        await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{surveyId}");
        await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}");

        var parameters = new
        {
            answer = answer.Answer,
            questionId = answer.Question_Id
        };
        await connection.ExecuteScalarAsync(answerCommand, parameters);
    }

    public async Task<bool> QuestionExist(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var response = await connection.ExecuteScalarAsync<int>(questionExistQuery, new { questionId });

        return response > 0;
    }

    public async Task<bool> QuestionTypeIsCorrect(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int questionTypeId = await connection.ExecuteScalarAsync<int>(getQuestionTypeQuery, new { questionId });

        return questionTypeId == 4;
    }

    public async Task<bool> AnswerExist(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int existingCount = await connection.ExecuteScalarAsync<int>(answerExistQuery, new { questionId });

        return existingCount > 0;
    }

    public async Task<bool> AnswerTypeIsCorrect(int questionId, string answer)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int textTypeId = await connection.ExecuteScalarAsync<int>(getTextTypeIdQuery, new { questionId });

        return ResponseTypeControl.ControlResponseType(answer, textTypeId);
    }

    public async Task<bool> RelationExist(int questionId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var response = await connection.ExecuteScalarAsync<int>(relationExistQuery, new { questionId });

        return response > 0;
    }
}
