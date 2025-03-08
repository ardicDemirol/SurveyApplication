using Dapper;
using Npgsql;

namespace SurveyApplication.Extensions;

public static class SequenceExtension
{
    public static async Task<int> GetNextIdAsync(this NpgsqlConnection connection, string tableName, string columnName, string foreignKeyAttributeName, int id)
    {
        string maxQuestionOrderQuery = $"SELECT COALESCE(MAX({columnName}), 0) FROM {tableName} WHERE {foreignKeyAttributeName} = @id;";
        return await connection.ExecuteScalarAsync<int>(maxQuestionOrderQuery, new { id });
    }
    public static async Task<int> GetNextIdAsync(this NpgsqlConnection connection, string tableName, string columnName)
    {
        string maxIdQuery = $"SELECT COALESCE(MAX({columnName}), 0) FROM {tableName};";
        int maxId = await connection.ExecuteScalarAsync<int>(maxIdQuery);
        return maxId + 1;
    }
}

