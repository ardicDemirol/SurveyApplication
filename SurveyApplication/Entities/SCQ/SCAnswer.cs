using SurveyApplication.Dtos.SingleChoiceDtos;

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

    public static SCAnswer FromDto(SCAnswerDto answerDto)
    {
        return new SCAnswer(answerDto.Answer_Id, answerDto.Answer, answerDto.Question_Id, answerDto.Survey_Id);
    }
}
