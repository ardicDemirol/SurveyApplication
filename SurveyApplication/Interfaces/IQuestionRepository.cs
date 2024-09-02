using SurveyApplication.Dtos.QuestionDtos;
using SurveyApplication.Views;

namespace SurveyApplication.Interfaces;
public interface IQuestionRepository
{
    Task CreateQuestion(QuestionDto question);
    Task<IEnumerable<SingleChoiceQuestionChoicesView>> GetAllQuestions<T>(int surveyId);

}


