using SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.TextBasedValidations;

public class TBSetRelationValidatorApp(ITextBasedRepository repository)
{
    private readonly ITextBasedRepository _repository = repository;


    public async Task QuestionExist(TBSetRelationCommandRequest request)
    {
        var response = await _repository.QuestionExist(request.Question_Id);

        if (!response)
        {
            throw new Exception($"Question Id {request.Question_Id} does not exist");
        }
    }

    public async Task QuestionTypeIsCorrect(TBSetRelationCommandRequest request)
    {
        var response = await _repository.QuestionTypeIsCorrect(request.Question_Id);

        if (!response)
        {
            throw new Exception("Question type is not free text ");
        }
    }

    public async Task RelationExist(TBSetRelationCommandRequest request)
    {
        var response = await _repository.RelationExist(request.Question_Id);

        if (response)
        {
            throw new Exception($"Relation already exist");
        }
    }

}


