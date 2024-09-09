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
                              VALUES (@answer,@questionId)
                              """;

    static readonly string setRelationCommand = """
                              INSERT INTO text_based_question_type_relationship (text_type_id,question_id) 
                              VALUES (@textTypeId,@questionId)
                              """;

    static readonly string getTextTypeCommand = """
                              SELECT text_type_id
                              FROM text_based_question_type_relationship
                              WHERE question_id = @questionId
                              """;

    static readonly string surveyIdCommand = """
                              SELECT survey_id
                              FROM question
                              WHERE question_id = @questionId
                              """;

    #endregion

    public async Task SetRelation(TextBasedQuestionTypeRelationshipDto relation)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var parameters = new
        {
            textTypeId = relation.Text_Type_Id,
            questionId = relation.Question_Id
        };

        await connection.ExecuteScalarAsync(setRelationCommand, parameters);
    }

    public async Task SaveAnswer(TextAnswersDto answer)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();


        int textTypeId = await connection.ExecuteScalarAsync<int>(getTextTypeCommand, new { questionId = answer.Question_Id });

        bool control = ResponseTypeControl.ControlResponseType(answer.Answer, textTypeId);

        if (!control) throw new ArgumentException("Answer type is not true");

        var parameters = new
        {
            answer = answer.Answer,
            questionId = answer.Question_Id
        };


        int surveyId = await connection.ExecuteScalarAsync<int>(surveyIdCommand, new { questionId = answer.Question_Id });


        await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{surveyId}");
        await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}");


        await connection.ExecuteScalarAsync(answerCommand, parameters);
    }

}
