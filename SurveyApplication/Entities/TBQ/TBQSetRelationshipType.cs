using SurveyApplication.Dtos.TextBasedDtos;

namespace SurveyApplication.Entities.TBQ;

public class TBQSetRelationshipType : Entity
{
    public int Relationship_Id { get; }
    public int Text_Type_Id { get; }
    public int Question_Id { get; }

    private TBQSetRelationshipType(int id, int textTypeId, int questionId) : base(id)
    {
        Text_Type_Id = textTypeId;
        Question_Id = questionId;
    }

    public static TBQSetRelationshipType Create(int id, int textTypeId, int questionId)
    {
        return new TBQSetRelationshipType(id, textTypeId, questionId);
    }

    public static TBQSetRelationshipType FromDto(TBQSetRelationshipTypeDto dto)
    {
        return new TBQSetRelationshipType(dto.Relationship_Id, dto.Text_Type_Id, dto.Question_Id);
    }
}
