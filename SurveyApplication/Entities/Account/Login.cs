using SurveyApplication.Dtos.Account;

namespace SurveyApplication.Entities.Account;
public class Login
{
    public string Email { get; }
    public string Password { get; }

    private Login(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public static Login Create(string email, string password)
    {
        return new Login(email, password);
    }
    public static Login FromDto(RegisterDto registerDto)
    {
        return new Login(registerDto.Email, registerDto.Password);
    }
}
