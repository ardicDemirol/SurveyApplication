using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.MultipleChoiceValidations;
public sealed class MCQSetMaxAnswerAmountValidatiorApp(IMultipleChoiceRepository repository)
{
    private readonly IMultipleChoiceRepository _repository = repository;

    public async Task SetMaxAmountExist(int questionId)
    {
        var setMaxAmountExists = await _repository.SetMaxAmountExist(questionId);

        if (setMaxAmountExists)
        {
            throw new Exception("Max amount already exists");
        }
    }
}