using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Entities.SCQ;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.SingleChoiceValidations;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;

public class SCSaveAnswerCommandHandle(ISingleChoiceRepository singleChoiceRepository, SCQSaveAnswerValidatorApp validator) : IRequestHandler<SCSaveAnswerCommandRequest, IActionResult>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;
    private readonly SCQSaveAnswerValidatorApp _validator = validator;

    public async Task<IActionResult> Handle(SCSaveAnswerCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.QuestionExist(request);
        await _validator.QuestionTypeIsCorrect(request);
        await _validator.ChoicesExist(request);
        await _validator.AnswerExist(request);
        await _validator.AnswerIsAnChoice(request);

        var newSingleChoiceAnswer = SCAnswer.Create(0, request.Answer, request.Question_Id, 0);
        await _singleChoiceRepository.SaveAnswer(newSingleChoiceAnswer.ToDto());

        return new OkObjectResult("Answer Saved Successfully");
    }
}
