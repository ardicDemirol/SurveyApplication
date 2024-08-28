namespace SurveyApplication.Features.Surveys.Queries.GetSurveyById;

public sealed record GetSurveyByIdQueryResponse(
    int Survey_Id,
    string Survey_Title,
    int Completed_Count
    );