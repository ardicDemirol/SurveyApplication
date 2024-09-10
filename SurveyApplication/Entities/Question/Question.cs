using SurveyApplication.Dtos.QuestionDtos;

namespace SurveyApplication.Entities.Question;
public class Question : Entity
{
    public string Question_Text { get; }
    public int Question_Order { get; }
    public char Question_Answer_Required { get; }
    public int Survey_Id { get; }
    public int Question_Type_Id { get; }

    private Question(
        int id,
        string questionText,
        char questionAnswerRequired,
        int surveyId,
        int questionTypeId
        ) : base(id)
    {
        Question_Text = questionText;
        Question_Answer_Required = questionAnswerRequired;
        Survey_Id = surveyId;
        Question_Type_Id = questionTypeId;
    }

    public static Question Create(
        int id,
        string questionText,
        char questionAnswerRequired,
        int surveyId,
        int questionTypeId
        )
    {
        return new Question(
            id,
            questionText,
            questionAnswerRequired,
            surveyId,
            questionTypeId);
    }
    public static Question FromDto(QuestionDto questionDto)
    {
        return new Question(
            questionDto.Question_Id,
            questionDto.Question_Text,
            questionDto.Question_Answer_Required,
            questionDto.Survey_Id,
            questionDto.Question_Type_Id);
    }
}
