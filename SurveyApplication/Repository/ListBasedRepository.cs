using Dapper;
using SurveyApplication.Dtos.ListBasedDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class ListBasedRepository(IDatabaseConnectionProvider databaseConnectionProvider) : IListBasedRepository
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;

    public async Task AddList(ListBasedQuestions list)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string insertCommand = """
                              INSERT INTO list_based_questions (question_id) 
                              VALUES (@questionId)
                              """;

        await connection.ExecuteAsync(insertCommand, new { questionId = list.Question_Id });
    }

    public Task AddListValue()
    {
        throw new NotImplementedException();
    }

    public Task SaveAnswer()
    {
        throw new NotImplementedException();
    }
}
