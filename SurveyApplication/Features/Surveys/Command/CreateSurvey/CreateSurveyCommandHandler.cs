using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Entities.Survey;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.SurveyValidations;

namespace SurveyApplication.Features.Surveys.Command.CreateSurvey;

public sealed class CreateSurveyCommandHandler(
    ISurveyRepository repository,
    CreateSurveyValidatorApp validator) : IRequestHandler<CreateSurveyCommandRequest, IActionResult>
{
    private readonly ISurveyRepository _repository = repository;
    private readonly CreateSurveyValidatorApp _validator = validator;

    public async Task<IActionResult> Handle(CreateSurveyCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.SurveyExist(request);

        var newSurvey = Survey.Create(0, request.Survey_Title, DateTime.Now, request.Finish_Time, 0, request.Company_Name);

        await _repository.CreateSurvey(newSurvey.ToDto());

        return new OkObjectResult("Survey Created Successfully");
    }
}
