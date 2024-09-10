using SurveyApplication.Dtos.MultipleChoiceDtos;

namespace SurveyApplication.Entities.MCQ;

public sealed class MCQSetMaxAnswerAmount : Entity
{
    public int Max_Choice_Amount { get; }
    public int Question_Id { get; }

    private MCQSetMaxAnswerAmount(int id, int maxChoiceAmount, int questionId) : base(id)
    {
        Max_Choice_Amount = maxChoiceAmount;
        Question_Id = questionId;
    }

    public static MCQSetMaxAnswerAmount Create(int id, int maxChoiceAmount, int questionId)
    {
        return new MCQSetMaxAnswerAmount(id, maxChoiceAmount, questionId);
    }

    public static MCQSetMaxAnswerAmount FromDto(MCSetMaxAnswerAmountDto setMaxChoiceAmountDto)
    {
        return new MCQSetMaxAnswerAmount(
            setMaxChoiceAmountDto.Choice_Id,
            setMaxChoiceAmountDto.Max_Choice_Amount,
            setMaxChoiceAmountDto.Question_Id);
    }
}



