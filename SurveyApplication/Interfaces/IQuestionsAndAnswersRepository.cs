using SurveyApplication.Dtos.QuestionsAndAnswers;

namespace SurveyApplication.Interfaces;
public interface IQuestionsAndAnswersRepository
{
    Task<IEnumerable<QuestionsAndAnswersViewDto>> GetAllQuestionsAndAnswers<T>(int surveyId);
}
