using FluentValidation;
using SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;

namespace SurveyApplication.Validations.TextBasedValidations;
public class TextBasedQuestionsSaveAnswerValidator : AbstractValidator<SaveAnswerCommandRequest>
{
    public TextBasedQuestionsSaveAnswerValidator()
    {
        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Answer cannot be empty")
            .MaximumLength(200).WithMessage("Answer cannot be more than 200 characters");


        RuleFor(x => x.Question_Id)
            .NotEmpty().WithMessage("Question Id cannot be empty");


    }
}
