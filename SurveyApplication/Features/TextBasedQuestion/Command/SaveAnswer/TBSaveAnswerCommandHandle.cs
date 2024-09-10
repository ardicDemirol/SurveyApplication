using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Entities.TBQ;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.TextBasedValidations;


namespace SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;

public class TBSaveAnswerCommandHandle(ITextBasedRepository textBasedRepository, TBSaveAnswerValidatorApp validator) : IRequestHandler<TBSaveAnswerCommandRequest, IActionResult>
{
    private readonly ITextBasedRepository _textBasedRepository = textBasedRepository;
    private readonly TBSaveAnswerValidatorApp _validator = validator;

    public async Task<IActionResult> Handle(TBSaveAnswerCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.QuestionExist(request);
        await _validator.SetRelationExist(request);
        await _validator.QuestionTypeIsCorrect(request);
        await _validator.AnswerTypeIsCorrect(request);
        await _validator.AnswerExist(request);

        var relation = TBQAnswers.Create(0, request.Answer, request.Question_Id);

        await _textBasedRepository.SaveAnswer(relation.ToDto());

        return new OkObjectResult("Answer Saved Successfully");
    }
}
