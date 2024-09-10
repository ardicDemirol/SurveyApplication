using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.MultipleChoiceValidations;
public class MCQAddChoiceValidatorApp(IMultipleChoiceRepository repository)
{
    private readonly IMultipleChoiceRepository _repository = repository;

    public async Task ChoiceExist(AddMCQChoiceCommandRequest request)
    {
        var mcqChoiceExists = await _repository.ChoiceExist(request.MultipleChoiceQuestionId, request.Choice);

        if (mcqChoiceExists)
        {
            throw new Exception("Choice already exists");
        }
    }
}
