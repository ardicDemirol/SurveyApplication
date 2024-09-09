using FluentValidation;
using SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;

namespace SurveyApplication.Validations.TextBasedValidations;
public class TextBasedQuestionsSaveAnswerValidator : AbstractValidator<SaveAnswerCommandRequest>
{
    public TextBasedQuestionsSaveAnswerValidator()
    {
        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .MaximumLength(200).WithMessage("{PropertyName} cannot be more than {MaxLength} characters");


        RuleFor(x => x.Question_Id)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");


    }
}
