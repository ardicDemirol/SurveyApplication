using MediatR;

namespace SurveyApplication.Features.Surveys.Queries.GetAllSurveys;

public class GetAllSurveysQueryRequest : IRequest<IList<GetAllSurveysQueryResponse>>
{
}
