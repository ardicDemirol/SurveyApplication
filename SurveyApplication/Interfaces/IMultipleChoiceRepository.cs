using SurveyApplication.Dtos.MultipleChoiceDtos;

namespace SurveyApplication.Interfaces;
public interface IMultipleChoiceRepository
{
    Task<int> SetMaxAnswerAmount(MultipleChoiceDto multipleChoiceModel);
    Task AddChoice(MultipleOtherChoicesDto addChoice);
    Task SaveAnswer(MultipleChoiceAnswersDto answerModel);
}
