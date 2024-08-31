using MediatR;

namespace SurveyApplication.Features.Surveys.Queries.GetSurveyById;

public sealed record GetSurveyByIdQueryRequest(int SurveyId) : IRequest<GetSurveyByIdQueryResponse>;

//public class GetSurveyByIdQueryRequest(int surveyId) : IRequest<GetSurveyByIdQueryResponse>
//{
//    public int Survey_Id { get; } = SurveyId;
//}



