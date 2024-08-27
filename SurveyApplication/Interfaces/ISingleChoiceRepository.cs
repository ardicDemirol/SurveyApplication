using SurveyApplication.Dtos;

namespace SurveyApplication.Interfaces;
public interface ISingleChoiceRepository
{
    Task AddChoice(SingleChoiceQuestionDto choice);
    Task<bool> SaveAnswer(SingleChoiceAnswerDto answer);
    Task<T> GetAnswer<T>(int questionId, int surveyId);
}
