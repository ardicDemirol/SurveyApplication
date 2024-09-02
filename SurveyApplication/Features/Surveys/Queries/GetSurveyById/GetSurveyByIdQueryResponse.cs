namespace SurveyApplication.Features.Surveys.Queries.GetSurveyById;

public sealed record GetSurveyByIdQueryResponse(string SurveyTitle, DateTime StartTime, DateTime FinishTime, int CompletedCount);


