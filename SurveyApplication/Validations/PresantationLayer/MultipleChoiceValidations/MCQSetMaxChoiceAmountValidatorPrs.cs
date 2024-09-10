using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

namespace SurveyApplication.Validations.PresantationLayer.MultipleChoiceValidations;
public class MCQSetMaxChoiceAmountValidatorPrs : AbstractValidator<SetMCQMaxAnswerAmountRequest>
{
    public MCQSetMaxChoiceAmountValidatorPrs()
    {
        RuleFor(request => request.QuestionId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(request => request.MaxChoiceAmount)
            .GreaterThan(0).WithMessage("{PropertyName} must be at least {ComparisonValue} character long.");
    }
}
