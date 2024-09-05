using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;

namespace SurveyApplication.Validations.MultipleChoiceValidations;

public class AddChoicesMCQValidator : AbstractValidator<AddMCQChoiceCommandRequest>
{
    public AddChoicesMCQValidator()
    {
        RuleFor(request => request.Choice)
            .NotEmpty().WithMessage("Choices must not be empty.")
            .MinimumLength(2).WithMessage("Choices must be at least 2 characters long.")
            .MaximumLength(20).WithMessage("Choices must be at most 20 characters long.");


        RuleFor(request => request.MultipleChoiceQuestionId)
            .NotEmpty().WithMessage("MultipleChoiceQuestionId must not be empty.")
            .GreaterThan(0).WithMessage("MultipleChoiceQuestionId must be greater than 0.");
    }
}
