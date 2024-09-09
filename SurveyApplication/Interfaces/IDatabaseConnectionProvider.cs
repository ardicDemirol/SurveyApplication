using Npgsql;

namespace SurveyApplication.Interfaces;
public interface IDatabaseConnectionProvider
{
    Task<NpgsqlConnection> ConnectAndOpenConnectionAsync();
}