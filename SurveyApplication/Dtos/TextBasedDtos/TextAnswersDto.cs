namespace SurveyApplication.Dtos.TextBasedDtos;
public sealed record TextAnswersDto
{
    public int Answer_Id { get; }
    public string Answer { get; }
    public int Question_Id { get; }

    private TextAnswersDto(string answer, int questionId)
    {
        Answer = answer;
        Question_Id = questionId;
    }

    public static TextAnswersDto Create(string answer, int questionId)
    {
        return new TextAnswersDto(answer, questionId);
    }
}

