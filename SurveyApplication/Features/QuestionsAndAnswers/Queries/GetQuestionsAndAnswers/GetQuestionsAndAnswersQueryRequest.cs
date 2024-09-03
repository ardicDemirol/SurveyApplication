using MediatR;

namespace SurveyApplication.Features.QuestionsAndAnswers.Queries.GetQuestionsAndAnswers;
public sealed record GetQuestionsAndAnswersQueryRequest(int Survey_Id) : IRequest<IList<GetQuestionsAndAnswersQueryResponse>>;
