using SurveyApplication.Dtos;

namespace SurveyApplication.Interfaces;
public interface IQuestionRepository
{
    Task CreateQuestion(QuestionDto question);
    Task<IEnumerable<T>> GetAllQuestions<T>(int surveyId);
}


