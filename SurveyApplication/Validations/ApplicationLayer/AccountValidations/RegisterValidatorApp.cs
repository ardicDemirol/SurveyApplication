using SurveyApplication.Features.Account.Command.Register;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.AccountValidations;

public sealed class RegisterValidatorApp(IAccountRepository repository)
{
    private readonly IAccountRepository _repository = repository;

    public async Task RegisterExist(RegisterCommandRequest request)
    {
        var userExists = await _repository.AccountExist(request.email);

        if (userExists)
        {
            throw new Exception("User with this email already exists");
        }
    }
}
