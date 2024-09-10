using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Entities.MCQ;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.MultipleChoiceValidations;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;

public class AddMCQChoicesCommandHandle(IMultipleChoiceRepository multipleChoiceRepository, MCQAddChoiceValidatorApp validator) : IRequestHandler<AddMCQChoiceCommandRequest, IActionResult>
{
    private readonly IMultipleChoiceRepository _multipleChoiceRepository = multipleChoiceRepository;
    private readonly MCQAddChoiceValidatorApp _validator = validator;

    public async Task<IActionResult> Handle(AddMCQChoiceCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.ChoiceExist(request);

        var newMultipleChoiceQuestionChoices = MCQSaveChoices.Create(0, request.Choice, request.MultipleChoiceQuestionId);

        await _multipleChoiceRepository.AddChoice(newMultipleChoiceQuestionChoices.ToDto());

        return new OkObjectResult("Choice Added Successfully");
    }
}
