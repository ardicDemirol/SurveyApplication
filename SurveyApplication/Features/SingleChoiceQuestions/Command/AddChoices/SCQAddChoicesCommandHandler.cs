using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Entities.SCQ;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.SingleChoiceValidations;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;

public class SCQAddChoicesCommandHandler(ISingleChoiceRepository repository, SCQAddChoiceValidatorApp validator) : IRequestHandler<SCQAddChoicesCommandRequest, IActionResult>
{
    private readonly ISingleChoiceRepository _repository = repository;
    private readonly SCQAddChoiceValidatorApp _validator = validator;

    public async Task<IActionResult> Handle(SCQAddChoicesCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.QuestionExist(request);
        await _validator.QuestionTypeIsCorrect(request);
        await _validator.ChoicesAreEquals(request);

        var newQuestionChoices = SCQuestion.Create(0, request.First_Choice, request.Second_Choice, request.Question_Id);

        await _repository.AddChoice(newQuestionChoices.ToDto());

        return new OkObjectResult("Choices Added Successfully");
    }
}
