namespace SurveyApplication.Entities.SCQ;

public class SCAnswer : Entity
{
    public string Answer { get; }
    public int Question_Id { get; }
    public int Survey_Id { get; }

    private SCAnswer(int id, string answer, int questionId, int surveyId) : base(id)
    {
        Answer = answer;
        Question_Id = questionId;
        Survey_Id = surveyId;
    }

    public static SCAnswer Create(int id, string answer, int questionId, int surveyId)
    {
        return new SCAnswer(id, answer, questionId, surveyId);
    }
}
