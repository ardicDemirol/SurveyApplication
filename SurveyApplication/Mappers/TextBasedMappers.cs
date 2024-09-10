using SurveyApplication.Dtos.TextBasedDtos;
using SurveyApplication.Entities.TBQ;

namespace SurveyApplication.Mappers;
public static class TextBasedMappers
{
    public static TBQAnswersDto ToDto(this TBQAnswers textAnswers)
    {
        return TBQAnswersDto.Create(textAnswers.Answer, textAnswers.Question_Id);
    }

    public static TBQSetRelationshipTypeDto ToDto(this TBQSetRelationshipType textBasedQuestion)
    {
        return TBQSetRelationshipTypeDto.Create(textBasedQuestion.Text_Type_Id, textBasedQuestion.Question_Id);
    }
}
