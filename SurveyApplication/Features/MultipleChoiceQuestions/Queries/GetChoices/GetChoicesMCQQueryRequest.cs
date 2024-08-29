using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Queries.GetChoices;

public sealed record GetChoicesMCQQueryRequest(int QuestionId) : IRequest<IList<GetChoicesMCQQueryResponse>>
{
    [Required] public int QuestionId { get; init; } = QuestionId;
}
