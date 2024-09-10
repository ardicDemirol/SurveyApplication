using SurveyApplication.Dtos.MultipleChoiceDtos;

namespace SurveyApplication.Interfaces;
public interface IMultipleChoiceRepository
{
    Task<int> SetMaxAnswerAmount(MCSetMaxAnswerAmountDto multipleChoiceModel);
    Task AddChoice(MCChoicesDto addChoice);
    Task SaveAnswer(MCAnswersDto answerModel);


    Task<bool> SetMaxAmountExist(int questionId);
    Task<bool> ChoiceExist(int questionId, string choice);
    Task<bool> AnswerExist(int questionId, string answer);
    Task<bool> AnswerIsAnChoice(int questionId, string answer);
    Task<bool> IsAnswerAmountWithinLimit(int questionId);
}
