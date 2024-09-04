using FluentValidation;
using SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;

namespace SurveyApplication.Validations.SingleChoiceValidations;

public class SaveSCAnswerValidator : AbstractValidator<SaveSCACommandRequest>
{
    public SaveSCAnswerValidator()
    {
        RuleFor(request => request.Answer)
            .NotEmpty().WithMessage("Answer must not be empty.")
            .MinimumLength(3).WithMessage("Answer must be at least 3 character long.")
            .MaximumLength(20).WithMessage("Answer cannot exceed 20 characters.");

        RuleFor(request => request.Question_Id)
            .GreaterThan(0).WithMessage("Question_Id must be greater than 0.");

        RuleFor(request => request.Survey_Id)
            .GreaterThan(0).WithMessage("Survey_Id must be greater than 0.");

    }
}
