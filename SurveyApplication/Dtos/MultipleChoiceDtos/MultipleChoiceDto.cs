namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public sealed record MultipleChoiceDto
{
    public int Choice_Id { get; }
    public int Max_Choice_Amount { get; }
    public int Question_Id { get; }

    private MultipleChoiceDto(int maxChoiceAmount, int questionId)
    {
        Max_Choice_Amount = maxChoiceAmount;
        Question_Id = questionId;
    }

    public static MultipleChoiceDto Create(int maxChoiceAmount, int questionId)
    {
        return new MultipleChoiceDto(maxChoiceAmount, questionId);
    }
}

