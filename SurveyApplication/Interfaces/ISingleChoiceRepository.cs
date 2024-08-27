using SurveyApplication.Dtos;

namespace SurveyApplication.Interfaces;
public interface ISingleChoiceRepository
{
    Task AddChoice(SingleChoiceQuestionDto choice);
    Task<T> SaveAnswer<T>(SingleChoiceAnswerDto answer);
    Task<T> GetAnswer<T>(int questionId, int surveyId);
}
