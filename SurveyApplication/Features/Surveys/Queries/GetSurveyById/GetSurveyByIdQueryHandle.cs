using MediatR;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Surveys.Queries.GetSurveyById;

public class GetSurveyByIdQueryHandle(ISurveyRepository repository) : IRequestHandler<GetSurveyByIdQueryRequest, GetSurveyByIdQueryResponse>
{
    private readonly ISurveyRepository _repository = repository;

    public async Task<GetSurveyByIdQueryResponse> Handle(GetSurveyByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var survey = await _repository.GetSurveyById<GetSurveyByIdQueryResponse>(request.SurveyId);

        return survey;
    }
}


