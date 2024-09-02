using MediatR;

namespace SurveyApplication.Features.SingleChoiceQuestions.Queries.GetAnswer;

public sealed record GetAnswerSCQQueryRequest(int QuestionID, int SurveyId) : IRequest<GetAnswerSCQQueryResponse>;


