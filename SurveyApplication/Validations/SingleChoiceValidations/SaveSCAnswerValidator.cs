using FluentValidation;
using SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;

namespace SurveyApplication.Validations.SingleChoiceValidations;

public class SaveSCAnswerValidator : AbstractValidator<SaveSCACommandRequest>
{
    public SaveSCAnswerValidator()
    {
        RuleFor(request => request.Answer)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .MinimumLength(3).WithMessage("{PropertyName} must be at least {MinLength} character long.")
            .MaximumLength(20).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");

        RuleFor(request => request.Question_Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(request => request.Survey_Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

    }
}
