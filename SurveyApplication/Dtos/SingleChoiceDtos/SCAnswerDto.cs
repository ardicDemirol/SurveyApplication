namespace SurveyApplication.Dtos.SingleChoiceDtos;

public sealed record SCAnswerDto
{
    public int Answer_Id { get; }
    public string Answer { get; }
    public int Question_Id { get; }
    public int Survey_Id { get; }

    private SCAnswerDto(string answer, int questionId, int surveyId)
    {
        Answer = answer;
        Question_Id = questionId;
        Survey_Id = surveyId;
    }

    public static SCAnswerDto Create(string answer, int questionId, int surveyId)
    {
        return new SCAnswerDto(answer, questionId, surveyId);
    }
}

