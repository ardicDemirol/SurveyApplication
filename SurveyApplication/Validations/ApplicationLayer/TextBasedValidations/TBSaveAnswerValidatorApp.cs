using SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.TextBasedValidations;

public class TBSaveAnswerValidatorApp(ITextBasedRepository repository)
{
    private readonly ITextBasedRepository _repository = repository;


    public async Task QuestionExist(TBSaveAnswerCommandRequest request)
    {
        var response = await _repository.QuestionExist(request.Question_Id);

        if (!response)
        {
            throw new Exception($"Question Id {request.Question_Id} does not exist");
        }
    }

    public async Task QuestionTypeIsCorrect(TBSaveAnswerCommandRequest request)
    {
        var response = await _repository.QuestionTypeIsCorrect(request.Question_Id);

        if (!response)
        {
            throw new Exception("Question type is not free text ");
        }
    }

    public async Task AnswerExist(TBSaveAnswerCommandRequest request)
    {
        var response = await _repository.AnswerExist(request.Question_Id);

        if (response)
        {
            throw new Exception("You answered this question before.");
        }
    }

    public async Task AnswerTypeIsCorrect(TBSaveAnswerCommandRequest request)
    {
        var response = await _repository.AnswerTypeIsCorrect(request.Question_Id, request.Answer);

        if (!response)
        {
            throw new Exception("Answer type is not true");
        }
    }

    public async Task SetRelationExist(TBSaveAnswerCommandRequest request)
    {
        var response = await _repository.RelationExist(request.Question_Id);

        if (!response)
        {
            throw new Exception("Relation does not exist");
        }
    }
}


