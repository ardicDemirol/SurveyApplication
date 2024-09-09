using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

namespace SurveyApplication.Validations.MultipleChoiceValidations;
public class SetMaxChoiceAmountValidator : AbstractValidator<SetMCQMaxAnswerAmountRequest>
{
    public SetMaxChoiceAmountValidator()
    {
        RuleFor(request => request.QuestionID)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(request => request.MaxChoiceAmount)
            .GreaterThan(0).WithMessage("{PropertyName} must be at least {ComparisonValue} character long.");
    }
}
