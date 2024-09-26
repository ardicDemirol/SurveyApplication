using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Interfaces;
using SurveyApplication.Validations.ApplicationLayer.AccountValidations;

namespace SurveyApplication.Features.Account.Command.Login;

public class LoginCommandHandle(
    IAccountRepository repository,
    IJWTProvider provider,
    LoginValidatorApp validator) : IRequestHandler<LoginCommandRequest, IActionResult>
{
    private readonly IJWTProvider _provider = provider;
    private readonly LoginValidatorApp _validator = validator;
    private readonly IAccountRepository _repository = repository;

    public async Task<IActionResult> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.LoginSuccessfully(request);
        var role = await _repository.GetRole(request.email);
        var token = _provider.GenerateJWTToken(request.email, role);

        return new OkObjectResult(new { Token = token });
    }
}