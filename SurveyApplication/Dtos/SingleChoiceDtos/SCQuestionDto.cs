namespace SurveyApplication.Dtos.SingleChoiceDtos;

public sealed record SCQuestionDto
{
    public int Choice_Id { get; }
    public string First_Choice { get; }
    public string Second_Choice { get; }
    public int Question_Id { get; }

    private SCQuestionDto(string firstChoice, string secondChoice, int questionId)
    {
        First_Choice = firstChoice;
        Second_Choice = secondChoice;
        Question_Id = questionId;
    }

    public static SCQuestionDto Create(string firstChoice, string secondChoice, int questionId)
    {
        return new SCQuestionDto(firstChoice, secondChoice, questionId);
    }
}

