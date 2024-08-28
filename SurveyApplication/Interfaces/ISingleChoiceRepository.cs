using SurveyApplication.Dtos;

namespace SurveyApplication.Interfaces;
public interface ISingleChoiceRepository
{
    Task AddChoices(SingleChoiceQuestionDto choice);
    Task SaveAnswer(SingleChoiceAnswerDto answer);
    Task<T> GetAnswer<T>(int questionId, int surveyId);
}
