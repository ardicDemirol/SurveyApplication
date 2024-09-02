using SurveyApplication.Dtos.QuestionDtos;

namespace SurveyApplication.Interfaces;
public interface IQuestionRepository
{
    Task CreateQuestion(QuestionDto question);
    Task<IEnumerable<QuestionChoicesViewDto>> GetAllQuestions<T>(int surveyId);

}


