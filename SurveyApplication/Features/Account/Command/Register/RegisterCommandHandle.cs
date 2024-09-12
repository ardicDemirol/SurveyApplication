using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.AccountValidations;

namespace SurveyApplication.Features.Account.Command.Register;

public class RegisterCommandHandle(
    IAccountRepository repository,
    RegisterValidatorApp validator) : IRequestHandler<RegisterCommandRequest, IActionResult>
{
    private readonly IAccountRepository _repository = repository;
    private readonly RegisterValidatorApp _validator = validator;

    public async Task<IActionResult> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.RegisterExist(request);

        var newRegister = Entities.Account.Register.Create(request.email, request.password, request.role);

        await _repository.Register(newRegister.ToDto());

        return new OkObjectResult("User Registered Successfully");
    }
}
