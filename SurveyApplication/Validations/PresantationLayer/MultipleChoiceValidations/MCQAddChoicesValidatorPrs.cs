using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;

namespace SurveyApplication.Validations.PresantationLayer.MultipleChoiceValidations;

public class MCQAddChoicesValidatorPrs : AbstractValidator<AddMCQChoiceCommandRequest>
{
    public MCQAddChoicesValidatorPrs()
    {
        RuleFor(request => request.Choice)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .MinimumLength(2).WithMessage("{PropertyName} must be at least {MinLength} characters long.")
            .MaximumLength(20).WithMessage("{PropertyName} must be at most {MaxLength} characters long.");


        RuleFor(request => request.MultipleChoiceQuestionId)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");
    }
}
