using MediatR;

namespace SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;

public sealed record GetAllSurveyQuestionsQueryRequest(int SurveyId) : IRequest<IList<GetAllSurveyQuestionsQueryResponse>>;



