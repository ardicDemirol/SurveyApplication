﻿using FluentValidation;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;

namespace SurveyApplication.Validations.MultipleChoiceValidations;

public class SaveAnswerMCQValidator : AbstractValidator<SaveMCACommandRequest>
{
    public SaveAnswerMCQValidator()
    {
        RuleFor(request => request.Answer)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .MinimumLength(1).WithMessage("{PropertyName} must be at least {MinLength} character long.")
            .MaximumLength(30).WithMessage("{PropertyName} cannot exceed {MaxLength} characters.");

        RuleFor(request => request.QuestionId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

    }
}
