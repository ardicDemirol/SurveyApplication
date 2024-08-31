using Dapper;
using SurveyApplication.Dtos.TextBasedDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class TextBasedRepository(IDatabaseConnectionProvider databaseConnectionProvider) : ITextBasedRepository
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;

    public async Task SetRelation(TextBasedQuestionTypeRelationshipDto relation)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string setRelationCommand = """
                              INSERT INTO text_based_question_type_relationship (text_type_id,question_id) 
                              VALUES (@textTypeId,@questionId)
                              """;

        var parameters = new
        {
            textTypeId = relation.Text_Type_Id,
            questionId = relation.Question_Id
        };

        await connection.ExecuteScalarAsync(setRelationCommand, parameters);
    }

    public async Task SaveAnswer(TextAnswersDto answer)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string setRelationCommand = """
                              INSERT INTO text_based_answer (answer,question_id) 
                              VALUES (@answer,@questionId)
                              """;

        string getTextTypeCommand = """
                              SELECT text_type_id
                              FROM text_based_question_type_relationship
                              WHERE question_id = @questionId
                              """;

        int textTypeId = await connection.ExecuteScalarAsync<int>(getTextTypeCommand, new { questionId = answer.Question_Id });

        bool control = ResponseTypeControl.ControlResponseType(answer.Answer, textTypeId);

        if (!control) throw new Exception("Answer type is not true");

        var parameters = new
        {
            answer = answer.Answer,
            questionId = answer.Question_Id
        };

        await connection.ExecuteScalarAsync(setRelationCommand, parameters);
    }

}
