using SurveyApplication.Dtos.SingleChoiceDtos;

namespace SurveyApplication.Interfaces;
public interface ISingleChoiceRepository
{
    Task AddChoice(SCQuestionDto choice);
    Task SaveAnswer(SCAnswerDto answer);

    Task<bool> QuestionExist(int questionId, string firstChoice, string secondChoice);
}
