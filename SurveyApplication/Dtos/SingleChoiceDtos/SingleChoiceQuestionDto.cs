namespace SurveyApplication.Dtos.SingleChoiceDtos;

public sealed record SingleChoiceQuestionDto
{
    public int Choice_Id { get; }
    public string First_Choice { get; }
    public string Second_Choice { get; }
    public int Question_Id { get; }

    private SingleChoiceQuestionDto(string firstChoice, string secondChoice, int questionId)
    {
        First_Choice = firstChoice;
        Second_Choice = secondChoice;
        Question_Id = questionId;
    }

    public static SingleChoiceQuestionDto Create(string firstChoice, string secondChoice, int questionId)
    {
        return new SingleChoiceQuestionDto(firstChoice, secondChoice, questionId);
    }
}

