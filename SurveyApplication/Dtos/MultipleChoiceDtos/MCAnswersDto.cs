namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public sealed record MCAnswersDto
{
    public int Answer_Id { get; }
    public string Answer { get; }
    public int Question_Id { get; }

    private MCAnswersDto(string answer, int questionId)
    {
        Answer = answer;
        Question_Id = questionId;
    }

    public static MCAnswersDto Create(string answer, int questionId)
    {
        return new MCAnswersDto(answer, questionId);
    }
}

