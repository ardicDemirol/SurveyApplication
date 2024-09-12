namespace SurveyApplication.Dtos.Account;
public sealed record LoginDto
{
    public string Email { get; }
    public string Password { get; }

    private LoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public static LoginDto Create(string email, string password)
    {
        return new LoginDto(email, password);
    }
}
