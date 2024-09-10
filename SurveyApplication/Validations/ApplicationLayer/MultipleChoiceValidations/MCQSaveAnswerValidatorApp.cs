using SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.MultipleChoiceValidations;

public class MCQSaveAnswerValidatorApp(IMultipleChoiceRepository repository)
{
    private readonly IMultipleChoiceRepository _repository = repository;

    public async Task AnswerExist(MCQSaveAnswerCommandRequest request)
    {
        var mcqAnswerExists = await _repository.AnswerExist(request.QuestionId, request.Answer);

        if (mcqAnswerExists)
        {
            throw new Exception($"{request.Answer} already exists");
        }
    }

    public async Task AnswerIsAnChoice(MCQSaveAnswerCommandRequest request)
    {
        var isAnswerOptions = await _repository.AnswerIsAnChoice(request.QuestionId, request.Answer);

        if (!isAnswerOptions)
        {
            throw new Exception($"{request.Answer} is not an option");
        }
    }


    public async Task IsAnswerAmountWithinLimit(MCQSaveAnswerCommandRequest request)
    {
        var isAnswerAmountWithinLimit = await _repository.IsAnswerAmountWithinLimit(request.QuestionId);

        if (!isAnswerAmountWithinLimit)
        {
            throw new Exception("Answer amount is within limit");
        }
    }

}
