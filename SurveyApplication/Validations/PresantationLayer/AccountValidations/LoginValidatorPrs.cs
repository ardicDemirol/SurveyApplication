using FluentValidation;
using SurveyApplication.Features.Account.Command.Login;

namespace SurveyApplication.Validations.PresantationLayer.AccountValidations;

public class LoginValidatorPrs : AbstractValidator<LoginCommandRequest>
{
    public LoginValidatorPrs()
    {
        RuleFor(request => request.email)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .EmailAddress().WithMessage("{PropertyName} must be a valid email address.")
            .MaximumLength(50).WithMessage("{PropertyName} must be at most {MaxLength} characters long.");

        RuleFor(request => request.password)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .MinimumLength(5).WithMessage("{PropertyName} must be at least {MinLength} characters long.")
            .MaximumLength(20).WithMessage("{PropertyName} must be at most {MaxLength} characters long.");
    }
}

