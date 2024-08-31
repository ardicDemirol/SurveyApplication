using SurveyApplication.Dtos.TextBasedDtos;

namespace SurveyApplication.Interfaces;
public interface ITextBasedRepository
{
    Task SetRelation(TextBasedQuestionTypeRelationshipDto relation);
    Task SaveAnswer(TextAnswersDto answer);
}
