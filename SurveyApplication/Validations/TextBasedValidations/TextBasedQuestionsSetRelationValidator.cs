using FluentValidation;
using SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;

namespace SurveyApplication.Validations.TextBasedValidations;
public class TextBasedQuestionsSetRelationValidator : AbstractValidator<SetRelationCommandRequest>
{
    public TextBasedQuestionsSetRelationValidator()
    {
        RuleFor(x => x.Text_Type_Id)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}")
            .LessThanOrEqualTo(3).WithMessage("{PropertyName} must be less than or equal to {MinLength}");

        RuleFor(x => x.Question_Id)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");
    }
}
