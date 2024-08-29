using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.Surveys.Queries.GetSurveyById;

public class GetSurveyByIdQueryRequest(int surveyId) : IRequest<GetSurveyByIdQueryResponse>
{
    [Required] public int SurveyId { get; } = surveyId;
}

