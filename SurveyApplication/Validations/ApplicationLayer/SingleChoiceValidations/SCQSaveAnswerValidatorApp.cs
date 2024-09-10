using SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.SingleChoiceValidations;
public class SCQSaveAnswerValidatorApp(ISingleChoiceRepository repository)
{
    private readonly ISingleChoiceRepository _repository = repository;

    public async Task QuestionExist(SCSaveAnswerCommandRequest request)
    {
        var response = await _repository.QuestionExist(request.Question_Id);

        if (!response)
        {
            throw new Exception($"Question Id {request.Question_Id} does not exist");
        }
    }

    public async Task ChoicesExist(SCSaveAnswerCommandRequest request)
    {
        var response = await _repository.ChoicesExist(request.Question_Id);

        if (!response)
        {
            throw new Exception($"The choices does not exist. The Question Id is {request.Question_Id}");
        }
    }

    public async Task QuestionTypeIsCorrect(SCSaveAnswerCommandRequest request)
    {
        var response = await _repository.QuestionTypeIsCorrect(request.Question_Id);

        if (!response)
        {
            throw new Exception("Question type is not single choice");
        }
    }
    public async Task AnswerIsAnChoice(SCSaveAnswerCommandRequest request)
    {
        var isAnswerOptions = await _repository.AnswerIsAnChoice(request.Question_Id, request.Answer);

        if (!isAnswerOptions)
        {
            throw new Exception($"{request.Answer} is not an option");
        }
    }

    public async Task AnswerExist(SCSaveAnswerCommandRequest request)
    {
        var scqAnswerExists = await _repository.AnswerExist(request.Question_Id);

        if (scqAnswerExists)
        {
            throw new Exception("You answered this question before.");
        }
    }

}
