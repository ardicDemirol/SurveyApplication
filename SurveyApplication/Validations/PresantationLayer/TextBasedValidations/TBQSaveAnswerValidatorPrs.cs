using FluentValidation;
using SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;

namespace SurveyApplication.Validations.PresantationLayer.TextBasedValidations;
public class TBQSaveAnswerValidatorPrs : AbstractValidator<TBSaveAnswerCommandRequest>
{
    public TBQSaveAnswerValidatorPrs()
    {
        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .MaximumLength(200).WithMessage("{PropertyName} cannot be more than {MaxLength} characters");


        RuleFor(x => x.Question_Id)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");


    }
}
