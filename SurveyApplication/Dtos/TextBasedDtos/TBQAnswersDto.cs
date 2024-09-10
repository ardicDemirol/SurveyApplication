namespace SurveyApplication.Dtos.TextBasedDtos;
public sealed record TBQAnswersDto
{
    public int Answer_Id { get; }
    public string Answer { get; }
    public int Question_Id { get; }

    private TBQAnswersDto(string answer, int questionId)
    {
        Answer = answer;
        Question_Id = questionId;
    }

    public static TBQAnswersDto Create(string answer, int questionId)
    {
        return new TBQAnswersDto(answer, questionId);
    }
}

