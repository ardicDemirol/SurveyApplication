using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Entities.MCQ;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.MultipleChoiceValidations;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;

public class MCQSaveAnswerCommandHandle(IMultipleChoiceRepository repository, MCQSaveAnswerValidatorApp validator) : IRequestHandler<MCQSaveAnswerCommandRequest, IActionResult>
{
    private readonly IMultipleChoiceRepository _repository = repository;
    private readonly MCQSaveAnswerValidatorApp _validator = validator;

    public async Task<IActionResult> Handle(MCQSaveAnswerCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.IsAnswerAmountWithinLimit(request);
        await _validator.AnswerIsAnChoice(request);
        await _validator.AnswerExist(request);

        var answer = MCQSaveAnswer.Create(0, request.Answer, request.QuestionId);

        await _repository.SaveAnswer(answer.ToDto());

        return new OkObjectResult("Answer Saved Successfully");
    }
}
