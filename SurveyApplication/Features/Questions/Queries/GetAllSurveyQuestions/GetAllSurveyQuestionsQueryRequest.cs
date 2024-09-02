using MediatR;

namespace SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;

public sealed record GetAllSurveyQuestionsQueryRequest(int Survey_Id) : IRequest<IList<GetAllSurveyQuestionsQueryResponse>>;



