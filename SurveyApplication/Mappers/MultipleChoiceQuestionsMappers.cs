using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Entities.MCQ;

namespace SurveyApplication.Mappers;
public static class MultipleChoiceQuestionsMappers
{
    public static MCSetMaxAnswerAmountDto ToDto(this MCQSetMaxAnswerAmount multipleChoice)
    {
        return MCSetMaxAnswerAmountDto.Create(multipleChoice.Max_Choice_Amount, multipleChoice.Question_Id);
    }

    public static MCChoicesDto ToDto(this MCQSaveChoices saveMCQChoices)
    {
        return MCChoicesDto.Create(saveMCQChoices.Choice, saveMCQChoices.Multiple_Choice_Question_Id);
    }

    public static MCAnswersDto ToDto(this MCQSaveAnswer saveMCQChoicesDto)
    {
        return MCAnswersDto.Create(saveMCQChoicesDto.Answer, saveMCQChoicesDto.Question_Id);
    }
}
