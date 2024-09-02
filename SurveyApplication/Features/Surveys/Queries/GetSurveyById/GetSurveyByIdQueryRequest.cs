using MediatR;

namespace SurveyApplication.Features.Surveys.Queries.GetSurveyById;

public sealed record GetSurveyByIdQueryRequest(int SurveyId) : IRequest<GetSurveyByIdQueryResponse>;




