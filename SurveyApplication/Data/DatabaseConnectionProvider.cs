using Npgsql;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Data;

public class DatabaseConnectionProvider(IConfiguration configuration) : IDatabaseConnectionProvider
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<NpgsqlConnection> ConnectAndOpenConnectionAsync()
    {
        var connectionString = Environment.GetEnvironmentVariable("SurveyAppPostgresConnectionString");
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }

}
