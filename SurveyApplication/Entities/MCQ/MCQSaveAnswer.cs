using SurveyApplication.Dtos.MultipleChoiceDtos;

namespace SurveyApplication.Entities.MCQ;
public class MCQSaveAnswer : Entity
{
    public string Answer { get; }
    public int Question_Id { get; }

    private MCQSaveAnswer(int id, string answer, int questionId) : base(id)
    {
        Answer = answer;
        Question_Id = questionId;
    }

    public static MCQSaveAnswer Create(int id, string answer, int questionId)
    {
        return new MCQSaveAnswer(id, answer, questionId);
    }

    public static MCQSaveAnswer FromDto(MCAnswersDto saveMCQChoicesDto)
    {
        return new MCQSaveAnswer(
            saveMCQChoicesDto.Answer_Id,
            saveMCQChoicesDto.Answer,
            saveMCQChoicesDto.Question_Id);
    }
}


