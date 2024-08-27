using Npgsql;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Data;

public class DatabaseConnectionProvider : IDatabaseConnectionProvider
{
    private readonly IConfiguration _configuration;
    public DatabaseConnectionProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<NpgsqlConnection> GetOpenConnectionAsync()
    {
        var connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresConnection"));
        await connection.OpenAsync();
        return connection;
    }

}
