using FluentValidation;
using SurveyApplication.Features.Questions.Command.CreateQuestion;

namespace SurveyApplication.Validations.PresantationLayer.QuestionValidations;
public class CreateQuestionValidatorPrs : AbstractValidator<CreateQuestionCommandRequest>
{
    public CreateQuestionValidatorPrs()
    {
        RuleFor(request => request.Question_Text)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .NotNull().WithMessage("{PropertyName} must not be null")
            .MinimumLength(10).WithMessage("{PropertyName} must be at least {MinLength} character long.")
            .MaximumLength(150).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");

        RuleFor(request => request.Question_Answer_Required)
            .NotEmpty().WithMessage("{PropertyName} required must not be empty.");


        RuleFor(request => request.Survey_Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(request => request.Question_Type_Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.")
            .LessThanOrEqualTo(4).WithMessage("{PropertyName} must be less than or equal to {ComparisonValue}.");

    }
}
