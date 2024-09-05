using FluentValidation;
using SurveyApplication.Features.Questions.Command.CreateQuestion;

namespace SurveyApplication.Validations.QuestionValidations;
public class CreateQuestionValidator : AbstractValidator<CreateQuestionCommandRequest>
{
    public CreateQuestionValidator()
    {
        RuleFor(request => request.Question_Text)
            .NotEmpty().WithMessage("Question text must not be empty.")
            .NotNull().WithMessage("Question text must not be null")
            .MinimumLength(10).WithMessage("Question text must be at least 10 character long.")
            .MaximumLength(150).WithMessage("Question text cannot exceed 150 characters.");

        RuleFor(request => request.Question_Answer_Required)
            .NotEmpty().WithMessage("Question answer required must not be empty.");


        RuleFor(request => request.Survey_Id)
            .GreaterThan(0).WithMessage("Survey Id must be greater than 0.");

        RuleFor(request => request.Question_Type_Id)
            .GreaterThan(0).WithMessage("Question Type Id must be greater than 0.")
            .LessThanOrEqualTo(4).WithMessage("Question Type Id must be less than or equal to 4.");

    }
}
