namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public sealed record MCSetMaxAnswerAmountDto
{
    public int Choice_Id { get; }
    public int Max_Choice_Amount { get; }
    public int Question_Id { get; }

    private MCSetMaxAnswerAmountDto(int maxChoiceAmount, int questionId)
    {
        Max_Choice_Amount = maxChoiceAmount;
        Question_Id = questionId;
    }

    public static MCSetMaxAnswerAmountDto Create(int maxChoiceAmount, int questionId)
    {
        return new MCSetMaxAnswerAmountDto(maxChoiceAmount, questionId);
    }
}

