using SurveyApplication.Dtos.QuestionDtos;

namespace SurveyApplication.Interfaces;
public interface IQuestionsAndAnswers
{
    Task<IEnumerable<QuestionChoicesViewDto>> GetAllQuestionsAndAnswers<T>(int surveyId);
}
