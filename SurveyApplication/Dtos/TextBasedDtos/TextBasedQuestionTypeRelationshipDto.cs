namespace SurveyApplication.Dtos.TextBasedDtos;

public sealed record TextBasedQuestionTypeRelationshipDto
{
    public int Relationship_Id { get; }
    public int Text_Type_Id { get; }
    public int Question_Id { get; }

    private TextBasedQuestionTypeRelationshipDto(int textTypeId, int questionId)
    {
        Text_Type_Id = textTypeId;
        Question_Id = questionId;
    }

    public static TextBasedQuestionTypeRelationshipDto Create(int textTypeId, int questionId)
    {
        return new TextBasedQuestionTypeRelationshipDto(textTypeId, questionId);
    }
}

