using Npgsql;
using System.Data;

namespace SurveyApplication.Data;

public class ApplicationDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgresConnection"));
    }


}
