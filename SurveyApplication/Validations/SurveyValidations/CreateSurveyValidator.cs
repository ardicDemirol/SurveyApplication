using FluentValidation;
using SurveyApplication.Features.Surveys.Command.CreateSurvey;

namespace SurveyApplication.Validations.SurveyValidations;
public class CreateSurveyValidator : AbstractValidator<CreateSurveyCommandRequest>
{
    public CreateSurveyValidator()
    {
        RuleFor(survey => survey.Survey_Title)
        .NotNull().WithMessage("Survey title cannot be null.")
        .NotEmpty().WithMessage("Survey title cannot be empty.")
        .MinimumLength(5).WithMessage("Survey title must be at least 5 characters long.")
        .MaximumLength(30).WithMessage("Survey title cannot exceed 30 characters.")
        .Must(title => !IsNumericOnly(title)).WithMessage("Survey title cannot consist only of numbers.");


        RuleFor(survey => survey.Finish_Time)
            .NotNull().WithMessage("Finish Time cannot be null.")
            .NotEmpty().WithMessage("Finish time cannot be empty.")
            .GreaterThan(DateTime.Now).WithMessage("Finish time must be after start time.");


        RuleFor(survey => survey.Company_Name)
            .NotNull().WithMessage("Company name cannot be null.")
            .NotEmpty().WithMessage("Company name cannot be empty.")
            .MinimumLength(5).WithMessage("Company name must be at least 5 characters long.")
            .MaximumLength(30).WithMessage("Company name cannot exceed 30 characters.");
    }

    private static bool IsNumericOnly(string? title) => title != null && title.All(char.IsDigit);

}
