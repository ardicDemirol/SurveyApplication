using MediatR;

namespace SurveyApplication.Features.SingleChoiceQuestions.Queries.GetAnswer;

public sealed record GetAnswerSCQQueryRequest(int QuestionID, int SurveyId) : IRequest<GetAnswerSCQQueryResponse>;


//public class GetAnswerSCQQueryRequest(int QuestionID, int Survey_Id) : IRequest<GetAnswerSCQQueryResponse>
//{
//    public int QuestionID { get; } = QuestionID;
//    public int Survey_Id { get; } = Survey_Id;
//}
