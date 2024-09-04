namespace SurveyApplication.Dtos.SingleChoiceDtos;

public sealed record SingleChoiceAnswerDto
{
    public int Answer_Id { get; }
    public string Answer { get; }
    public int Question_Id { get; }
    public int Survey_Id { get; }

    private SingleChoiceAnswerDto(string answer, int questionId, int surveyId)
    {
        Answer = answer;
        Question_Id = questionId;
        Survey_Id = surveyId;
    }

    public static SingleChoiceAnswerDto Create(string answer, int questionId, int surveyId)
    {
        if (string.IsNullOrWhiteSpace(answer)) throw new ArgumentException("Answer cannot be empty.");

        return new SingleChoiceAnswerDto(answer, questionId, surveyId);
    }
}

