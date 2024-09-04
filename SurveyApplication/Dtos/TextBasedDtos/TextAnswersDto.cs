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
        if (string.IsNullOrWhiteSpace(answer)) throw new ArgumentException("Answer cannot be empty.");

        if (questionId <= 0) throw new ArgumentException("Question ID must be greater than zero.");

        return new TextAnswersDto(answer, questionId);
    }
}

