namespace SurveyApplication.Dtos.SingleChoiceDtos;

public sealed record SCAnswerDto
{
    public int Answer_Id { get; }
    public string Answer { get; }
    public int Question_Id { get; }
    public int Survey_Id { get; }

    private SCAnswerDto(string answer, int questionId)
    {
        Answer = answer;
        Question_Id = questionId;
    }

    public static SCAnswerDto Create(string answer, int questionId)
    {
        return new SCAnswerDto(answer, questionId);
    }
}

