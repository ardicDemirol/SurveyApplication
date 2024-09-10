using SurveyApplication.Dtos.SingleChoiceDtos;
using SurveyApplication.Entities.SCQ;

namespace SurveyApplication.Mappers;
public static class SingleChoiceQuestionsMappers
{
    public static SCQuestionDto ToDto(this SCQuestion question)
    {
        return SCQuestionDto.Create(question.First_Choice, question.Second_Choice, question.Question_Id);
    }

    public static SCAnswerDto ToDto(this SCAnswer answer)
    {
        return SCAnswerDto.Create(answer.Answer, answer.Question_Id);
    }
}
