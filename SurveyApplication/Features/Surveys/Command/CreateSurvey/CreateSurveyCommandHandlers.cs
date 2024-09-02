using MediatR;
using SurveyApplication.Dtos.SurveyDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Surveys.Command.CreateSurvey;

public class CreateSurveyCommandHandlers(ISurveyRepository repository) : IRequestHandler<CreateSurveyCommandRequest>
{
    private readonly ISurveyRepository _repository = repository;


    public async Task Handle(CreateSurveyCommandRequest request, CancellationToken cancellationToken)
    {
        var newSurvey = new SurveyDto
        {
            Survey_Title = request.Survey_Title,
            Finish_Time = request.Finish_Time,
            Company_Name = request.Company_Name
        };
        await _repository.CreateSurvey<CreateSurveyCommandRequest>(newSurvey);
    }
}
