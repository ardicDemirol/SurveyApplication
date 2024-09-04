using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;

namespace SurveyApplication.Validations.MultipleChoiceValidations;

public class SaveAnswerMCQValidator : AbstractValidator<SaveMCACommandRequest>
{
    public SaveAnswerMCQValidator()
    {
        RuleFor(request => request.Answer)
            .NotEmpty().WithMessage("Answer must not be empty.")
            .MinimumLength(1).WithMessage("Answer must be at least 1 character long.")
            .MaximumLength(30).WithMessage("Answer cannot exceed 30 characters.");

        RuleFor(request => request.QuestionId)
            .GreaterThan(0).WithMessage("Question_Id must be greater than 0.");

    }
}
