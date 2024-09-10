using SurveyApplication.Dtos.SingleChoiceDtos;

namespace SurveyApplication.Entities.SCQ;
public class SCQuestion : Entity
{
    public string First_Choice { get; }
    public string Second_Choice { get; }
    public int Question_Id { get; }

    private SCQuestion(int id, string firstChoice, string secondChoice, int questionId) : base(id)
    {
        First_Choice = firstChoice;
        Second_Choice = secondChoice;
        Question_Id = questionId;
    }

    public static SCQuestion Create(int id, string firstChoice, string secondChoice, int questionId)
    {
        return new SCQuestion(id, firstChoice, secondChoice, questionId);
    }

    public static SCQuestion FromDto(SCQuestionDto scQuestionDto)
    {
        return new SCQuestion(
            scQuestionDto.Choice_Id,
            scQuestionDto.First_Choice,
            scQuestionDto.Second_Choice,
            scQuestionDto.Question_Id);
    }
}
