using SurveyApplication.Dtos.QuestionDtos;
using SurveyApplication.Entities.Question;

namespace SurveyApplication.Mappers;
public static class QuestionMapper
{
    public static QuestionDto ToDto(this Question question)
    {
        return QuestionDto.Create(question.Question_Text, question.Question_Answer_Required, question.Survey_Id, question.Question_Type_Id);
    }
}
