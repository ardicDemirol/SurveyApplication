using Dapper;
using SurveyApplication.Dtos.Account;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;
public class AccountRepository(IDatabaseConnectionProvider databaseConnectionProvider) : IAccountRepository
{
    #region Fields

    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;

    private static readonly string registerCommand = """
        INSERT INTO accounts (email, password,role)
        VALUES (:email, :password, :role);
        """;

    private static readonly string loginCommand = """
        SELECT COUNT(1)
        FROM accounts
        WHERE email = :email AND password = :password;
        """;

    private static readonly string accountExistCommand = """
        SELECT COUNT(1)
        FROM accounts
        WHERE email = :email;
        """;

    private static readonly string getRoleQuery = """
        SELECT role 
        FROM accounts 
        WHERE email = :email;
        """;

    #endregion

    public async Task Register(RegisterDto registerDto)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var parameters = new
        {
            email = registerDto.Email,
            password = registerDto.Password,
            role = registerDto.Role
        };

        await connection.ExecuteAsync(registerCommand, parameters);
    }

    public async Task<bool> AccountExist(string email)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        return await connection.ExecuteScalarAsync<bool>(accountExistCommand, new { email });
    }

    public async Task<bool> LoginSuccessfully(string email, string password)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        return await connection.ExecuteScalarAsync<bool>(loginCommand, new { email, password });
    }

    public async Task<string> GetRole(string email)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        return await connection.ExecuteScalarAsync<string>(getRoleQuery, new { email }) ?? string.Empty;
    }

}
