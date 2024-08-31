using MediatR;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Queries.GetChoices;

public sealed record GetChoicesMCQQueryRequest(int QuestionId) : IRequest<IList<GetChoicesMCQQueryResponse>>;
