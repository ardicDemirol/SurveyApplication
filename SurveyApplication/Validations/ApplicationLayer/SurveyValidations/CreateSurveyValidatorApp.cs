using SurveyApplication.Features.Surveys.Command.CreateSurvey;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.SurveyValidations;

public sealed class CreateSurveyValidatorApp(ISurveyRepository repository)
{
    private readonly ISurveyRepository _repository = repository;
    public async Task SurveyExist(CreateSurveyCommandRequest request)
    {
        var surveyExists = await _repository.SurveyExist(request.Survey_Title);

        if (surveyExists)
        {
            throw new Exception("Survey with this title already exists");
        }

    }
}