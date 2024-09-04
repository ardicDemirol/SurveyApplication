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
        if (textTypeId <= 0) throw new ArgumentException("Text Type ID must be greater than zero.");

        if (questionId <= 0) throw new ArgumentException("Question ID must be greater than zero.");

        return new TextBasedQuestionTypeRelationshipDto(textTypeId, questionId);
    }
}

