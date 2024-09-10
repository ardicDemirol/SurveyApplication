using SurveyApplication.Dtos.TextBasedDtos;

namespace SurveyApplication.Entities.TBQ;

public class TBQAnswers : Entity
{
    public string Answer { get; }
    public int Question_Id { get; }

    private TBQAnswers(int id, string answer, int questionId) : base(id)
    {
        Answer = answer;
        Question_Id = questionId;
    }

    public static TBQAnswers Create(int id, string answer, int questionId)
    {
        return new TBQAnswers(id, answer, questionId);
    }

    public static TBQAnswers FromDto(TBQAnswersDto dto)
    {
        return new TBQAnswers(dto.Answer_Id, dto.Answer, dto.Question_Id);
    }
}
