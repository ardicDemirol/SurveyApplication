using MediatR;

namespace SurveyApplication.Features.Surveys.Queries.GetSurveyById;

public class GetSurveyByIdQueryRequest(int surveyId) : IRequest<GetSurveyByIdQueryResponse>
{
    public int SurveyId { get; } = surveyId;
}

