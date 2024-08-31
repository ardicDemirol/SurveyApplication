namespace SurveyApplication.Features.Surveys.Queries.GetSurveyById;

public sealed record GetSurveyByIdQueryResponse(int SurveyId, string SurveyTitle, int CompletedCount);


