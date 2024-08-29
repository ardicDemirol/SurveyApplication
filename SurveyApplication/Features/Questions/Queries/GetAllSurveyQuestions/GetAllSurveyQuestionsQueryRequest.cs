using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;

public class GetAllSurveyQuestionsQueryRequest(int SurveyId) : IRequest<IList<GetAllSurveyQuestionsQueryResponse>>
{
    [Required] public int SurveyId { get; init; } = SurveyId;
}
