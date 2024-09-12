using SurveyApplication.Dtos.Account;
using SurveyApplication.Entities.Account;

namespace SurveyApplication.Mappers;
public static class AccountMappers
{
    public static RegisterDto ToDto(this Register request)
    {
        return RegisterDto.Create(request.Email, request.Password, request.Role);
    }

    public static LoginDto ToDto(this Login request)
    {
        return LoginDto.Create(request.Email, request.Password);
    }
}
