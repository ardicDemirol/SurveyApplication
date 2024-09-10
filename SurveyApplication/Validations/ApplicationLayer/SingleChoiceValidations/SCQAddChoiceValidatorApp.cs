using SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.SingleChoiceValidations;
public class SCQAddChoiceValidatorApp(ISingleChoiceRepository repository)
{
    private readonly ISingleChoiceRepository _repository = repository;

    public async Task QuestionExist(SCQAddChoicesCommandRequest request)
    {
        var response = await _repository.QuestionExist(request.Question_Id);

        if (!response)
        {
            throw new Exception($"Question Id {request.Question_Id} does not exist");
        }
    }

    public async Task QuestionTypeIsCorrect(SCQAddChoicesCommandRequest request)
    {
        var response = await _repository.QuestionTypeIsCorrect(request.Question_Id);

        if (!response)
        {
            throw new Exception("Question type is not single choice");
        }
    }

    public async Task ChoicesAreEquals(SCQAddChoicesCommandRequest request)
    {
        var response = await _repository.ChoicesAreEquals(request.First_Choice, request.Second_Choice);

        if (response)
        {
            throw new Exception("Choices cannot be equal");
        }
    }

}
