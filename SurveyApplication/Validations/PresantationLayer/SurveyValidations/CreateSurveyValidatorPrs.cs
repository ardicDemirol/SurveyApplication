using FluentValidation;
using SurveyApplication.Features.Surveys.Command.CreateSurvey;

namespace SurveyApplication.Validations.PresantationLayer.SurveyValidations;
public class CreateSurveyValidatorPrs : AbstractValidator<CreateSurveyCommandRequest>
{
    public CreateSurveyValidatorPrs()
    {
        RuleFor(survey => survey.Survey_Title)
        .NotNull().WithMessage("{PropertyName} cannot be null.")
        .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
        .MinimumLength(5).WithMessage("{PropertyName} must be at least {MinLength} characters long.")
        .MaximumLength(30).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.")
        .Must(title => !IsNumericOnly(title)).WithMessage("{PropertyName} cannot consist only of numbers.");


        RuleFor(survey => survey.Finish_Time)
            .NotNull().WithMessage("{PropertyName} cannot be null.")
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .GreaterThan(DateTime.Now).WithMessage("{PropertyName} must be after start time.");


        RuleFor(survey => survey.Company_Name)
            .NotNull().WithMessage("{PropertyName} cannot be null.")
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .MinimumLength(5).WithMessage("{PropertyName} must be at least {MinLength} characters long.")
            .MaximumLength(30).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");
    }

    private static bool IsNumericOnly(string? title) => title is not null && title.All(char.IsDigit);

}
