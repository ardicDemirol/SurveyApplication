using FluentValidation;
using SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;

namespace SurveyApplication.Validations.SingleChoiceValidations;

public class AddSCQuestionValidator : AbstractValidator<AddSCQChoicesCommandRequest>
{
    public AddSCQuestionValidator()
    {
        RuleFor(request => request.First_Choice)
            .NotEmpty().WithMessage("Choice must not be empty.")
            .MinimumLength(3).WithMessage("Choice must be at least 3 character long.")
            .MaximumLength(20).WithMessage("Choice cannot exceed 20 characters.");

        RuleFor(request => request.Second_Choice)
           .NotEmpty().WithMessage("Choice must not be empty.")
           .MinimumLength(3).WithMessage("Choice must be at least 3 character long.")
           .MaximumLength(20).WithMessage("Choice cannot exceed 20 characters.");


        RuleFor(request => request.Question_Id)
            .GreaterThan(0).WithMessage("MultipleChoiceQuestionId must be greater than 0.");

    }
}
