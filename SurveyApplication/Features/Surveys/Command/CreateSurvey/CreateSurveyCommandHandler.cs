using MediatR;
using SurveyApplication.Dtos.SurveyDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Surveys.Command.CreateSurvey;

public class CreateSurveyCommandHandler(ISurveyRepository repository) : IRequestHandler<CreateSurveyCommandRequest>
{
    private readonly ISurveyRepository _repository = repository;

    public async Task Handle(CreateSurveyCommandRequest request, CancellationToken cancellationToken)
    {
        var newSurvey = SurveyDto.Create(request.Survey_Title, DateTime.Now, request.Finish_Time, 0, request.Company_Name);

        await _repository.CreateSurvey<CreateSurveyCommandRequest>(newSurvey);
    }
}
