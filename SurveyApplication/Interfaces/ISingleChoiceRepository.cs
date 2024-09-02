using SurveyApplication.Dtos.SingleChoiceDtos;

namespace SurveyApplication.Interfaces;
public interface ISingleChoiceRepository
{
    Task AddChoice(SingleChoiceQuestionDto choice);
    Task SaveAnswer(SingleChoiceAnswerDto answer);
    Task<T> GetAnswer<T>(int questionId);
}
