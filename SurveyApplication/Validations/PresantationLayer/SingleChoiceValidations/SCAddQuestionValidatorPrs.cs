using FluentValidation;
using SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;

namespace SurveyApplication.Validations.PresantationLayer.SingleChoiceValidations;

public class SCAddQuestionValidatorPrs : AbstractValidator<SCQAddChoicesCommandRequest>
{
    public SCAddQuestionValidatorPrs()
    {
        RuleFor(request => request.First_Choice)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .MinimumLength(3).WithMessage("{PropertyName} must be at least {MinLength} character long.")
            .MaximumLength(20).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");

        RuleFor(request => request.Second_Choice)
           .NotEmpty().WithMessage("{PropertyName} must not be empty.")
           .MinimumLength(3).WithMessage("{PropertyName} must be at least {MinLength} character long.")
           .MaximumLength(20).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");


        RuleFor(request => request.Question_Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

    }
}
