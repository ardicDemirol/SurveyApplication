using MediatR;

namespace SurveyApplication.Features.Surveys.Queries.GetAnswerSingleChoiceQuestions;

public class GetAnswerSingleChoiceQuestionsQueryRequest(int questionID, int surveyID) : IRequest<GetAnswerSingleChoiceQuestionsQueryResponse>
{
    public int QuestionID { get; } = questionID;
    public int SurveyID { get; } = surveyID;
}
