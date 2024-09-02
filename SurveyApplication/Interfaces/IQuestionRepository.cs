using SurveyApplication.Dtos.QuestionDtos;

namespace SurveyApplication.Interfaces;
public interface IQuestionRepository
{
    Task CreateQuestion(QuestionDto question);
    Task<IEnumerable<SingleChoiceQuestionChoicesViewDto>> GetAllQuestions<T>(int surveyId);

}


