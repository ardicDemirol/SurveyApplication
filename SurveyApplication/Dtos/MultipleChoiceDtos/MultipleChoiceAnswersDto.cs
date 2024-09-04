namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public sealed record MultipleChoiceAnswersDto
{
    public int Answer_Id { get; }
    public string Answer { get; }
    public int Question_Id { get; }

    private MultipleChoiceAnswersDto(string answer, int questionId)
    {
        Answer = answer;
        Question_Id = questionId;
    }

    public static MultipleChoiceAnswersDto Create(string answer, int questionId)
    {
        if (string.IsNullOrWhiteSpace(answer)) throw new ArgumentException("Answer cannot be empty.");

        return new MultipleChoiceAnswersDto(answer, questionId);
    }
}

