using FluentValidation;
using SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;

namespace SurveyApplication.Validations.TextBasedValidations;
public class TextBasedQuestionsSetRelationValidator : AbstractValidator<SetRelationCommandRequest>
{
    public TextBasedQuestionsSetRelationValidator()
    {
        RuleFor(x => x.Text_Type_Id)
            .NotEmpty().WithMessage("Text type id cannot be empty")
            .GreaterThan(0).WithMessage("Text type id must be greater than 0")
            .LessThanOrEqualTo(3).WithMessage("Text type id must be less than or equal to 3");

        RuleFor(x => x.Question_Id)
            .NotEmpty().WithMessage("Question Id cannot be empty")
            .GreaterThan(0).WithMessage("Text type id must be greater than 0");
    }
}
