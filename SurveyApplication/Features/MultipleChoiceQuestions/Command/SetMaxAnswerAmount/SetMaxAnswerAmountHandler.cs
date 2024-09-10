using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.MultipleChoiceValidations;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

public class SetMaxAnswerAmountHandler(
    IMultipleChoiceRepository repository,
    MCQSetMaxAnswerAmountValidatiorApp validator)
    : IRequestHandler<SetMCQMaxAnswerAmountRequest, IActionResult>
{
    private readonly IMultipleChoiceRepository _repository = repository;
    private readonly MCQSetMaxAnswerAmountValidatiorApp _validator = validator;

    public async Task<IActionResult> Handle(SetMCQMaxAnswerAmountRequest request, CancellationToken cancellationToken)
    {
        await _validator.SetMaxAmountExist(request.QuestionId);

        var question = Entities.MCQ.MCQSetMaxAnswerAmount.Create(0, request.MaxChoiceAmount, request.QuestionId);

        await _repository.SetMaxAnswerAmount(question.ToDto());

        return new OkObjectResult("Max Answer Amount Successfully Saved");

        // Before choice is is returning but not anymore
    }
}





