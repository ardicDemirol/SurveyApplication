using SurveyApplication.Dtos.QuestionDtos;

namespace SurveyApplication.Interfaces;
public interface IQuestionRepository
{
    Task CreateQuestion(QuestionDto question);
    Task<IEnumerable<QuestionChoicesViewDto>> GetAllSurveyQuestions<T>(int surveyId);
    Task<bool> QuestionExist(int surveyId, string questionText);

}


