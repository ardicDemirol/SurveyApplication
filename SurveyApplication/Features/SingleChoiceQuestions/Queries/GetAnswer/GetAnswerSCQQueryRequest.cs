using MediatR;

namespace SurveyApplication.Features.SingleChoiceQuestions.Queries.GetAnswer;

public class GetAnswerSCQQueryRequest(int questionID, int surveyID) : IRequest<GetAnswerSCQQueryResponse>
{
    public int QuestionID { get; } = questionID;
    public int SurveyID { get; } = surveyID;
}
