using SurveyApplication.Dtos.Account;

namespace SurveyApplication.Entities.Account;

public class Register
{
    public string Email { get; }
    public string Password { get; }
    public string Role { get; }

    private Register(string email, string password, string role)
    {
        Email = email;
        Password = password;
        Role = role;
    }

    public static Register Create(string email, string password, string role)
    {
        return new Register(email, password, role);
    }
    public static Register FromDto(RegisterDto registerDto)
    {
        return new Register(registerDto.Email, registerDto.Password, registerDto.Role);
    }
}
