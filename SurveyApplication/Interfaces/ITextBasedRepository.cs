using SurveyApplication.Dtos.TextBasedDtos;

namespace SurveyApplication.Interfaces;
public interface ITextBasedRepository
{
    Task SetRelation(TBQSetRelationshipTypeDto relation);
    Task SaveAnswer(TBQAnswersDto answer);

    Task<bool> QuestionExist(int questionId);
    Task<bool> QuestionTypeIsCorrect(int questionId);
    Task<bool> AnswerTypeIsCorrect(int questionId, string answer);
    Task<bool> AnswerExist(int questionId);
    Task<bool> RelationExist(int questionId);

}
