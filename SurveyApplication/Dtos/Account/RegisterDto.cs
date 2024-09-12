namespace SurveyApplication.Dtos.Account;

public sealed record RegisterDto
{
    public string Email { get; }
    public string Password { get; }

    public string Role { get; }

    private RegisterDto(string email, string password, string role)
    {
        Email = email;
        Password = password;
        Role = role;
    }

    public static RegisterDto Create(string email, string password, string role)
    {
        return new RegisterDto(email, password, role);
    }

}
