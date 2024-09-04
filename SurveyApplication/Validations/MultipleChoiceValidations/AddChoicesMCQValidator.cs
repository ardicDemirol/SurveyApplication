using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;

namespace SurveyApplication.Validations.MultipleChoiceValidations;

public class AddChoicesMCQValidator : AbstractValidator<AddMCQChoiceCommandRequest>
{
    public AddChoicesMCQValidator()
    {
        RuleFor(request => request.Choice)
            .NotEmpty().WithMessage("Choices must not be empty.");

        RuleFor(request => request.MultipleChoiceQuestionId)
            .GreaterThan(0).WithMessage("MultipleChoiceQuestionId must be greater than 0.");
    }
}
