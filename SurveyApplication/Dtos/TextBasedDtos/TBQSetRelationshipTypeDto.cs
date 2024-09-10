namespace SurveyApplication.Dtos.TextBasedDtos;

public sealed record TBQSetRelationshipTypeDto
{
    public int Relationship_Id { get; }
    public int Text_Type_Id { get; }
    public int Question_Id { get; }

    private TBQSetRelationshipTypeDto(int textTypeId, int questionId)
    {
        Text_Type_Id = textTypeId;
        Question_Id = questionId;
    }

    public static TBQSetRelationshipTypeDto Create(int textTypeId, int questionId)
    {
        return new TBQSetRelationshipTypeDto(textTypeId, questionId);
    }
}

