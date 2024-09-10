using SurveyApplication.Dtos.SingleChoiceDtos;

namespace SurveyApplication.Interfaces;
public interface ISingleChoiceRepository
{
    Task AddChoice(SCQuestionDto choice);
    Task SaveAnswer(SCAnswerDto answer);

    Task<bool> QuestionExist(int questionId);
    Task<bool> QuestionTypeIsCorrect(int questionId);
    Task<bool> ChoicesAreEquals(string firstChoice, string secondChoice);
    Task<bool> ChoicesExist(int questionId);
    Task<bool> AnswerIsAnChoice(int questionId, string answer);
    Task<bool> AnswerExist(int questionId);
}
