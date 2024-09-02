using MediatR;

namespace SurveyApplication.Features.Surveys.Queries.GetAllSurveys;

public sealed record GetAllSurveysQueryRequest : IRequest<IList<GetAllSurveysQueryResponse>>;
