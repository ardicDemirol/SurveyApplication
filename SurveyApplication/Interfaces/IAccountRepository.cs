using SurveyApplication.Dtos.Account;

namespace SurveyApplication.Interfaces;
public interface IAccountRepository
{
    Task Register(RegisterDto registerDto);
    Task<bool> AccountExist(string email);
    Task<bool> LoginSuccessfully(string email, string password);
    Task<string> GetRole(string email);
}
