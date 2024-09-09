using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;

namespace SurveyApplication.Validations.MultipleChoiceValidations;

public class AddChoicesMCQValidator : AbstractValidator<AddMCQChoiceCommandRequest>
{
    public AddChoicesMCQValidator()
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
