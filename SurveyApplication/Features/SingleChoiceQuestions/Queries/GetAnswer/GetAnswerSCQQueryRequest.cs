using MediatR;

namespace SurveyApplication.Features.SingleChoiceQuestions.Queries.GetAnswer;

public sealed record GetAnswerSCQQueryRequest(int QuestionID) : IRequest<GetAnswerSCQQueryResponse>;


