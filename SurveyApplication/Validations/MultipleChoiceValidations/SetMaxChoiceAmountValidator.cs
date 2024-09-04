using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

namespace SurveyApplication.Validations.MultipleChoiceValidations;
public class SetMaxChoiceAmountValidator : AbstractValidator<SetMCQMaxAnswerAmountRequest>
{
    public SetMaxChoiceAmountValidator()
    {
        RuleFor(request => request.QuestionID)
            .GreaterThan(0).WithMessage("Question Id must be greater than 0.");

        RuleFor(request => request.MaxChoiceAmount)
            .GreaterThan(0).WithMessage("Max choice amount must be greater than 0.");
    }
}
