using SurveyApplication.Features.Account.Command.Login;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.AccountValidations;

public sealed class LoginValidatorApp(IAccountRepository repository)
{
    private readonly IAccountRepository _repository = repository;

    public async Task LoginSuccessfully(LoginCommandRequest request)
    {
        var successfully = await _repository.LoginSuccessfully(request.email, request.password);

        if (!successfully)
        {
            throw new Exception("Login Failed");
        }
    }

}
