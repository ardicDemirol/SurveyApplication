using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos.TextBasedDtos;

public class TextBasedQuestionTypeRelationshipDto
{
    [Key]
    public int Relationship_Id { get; set; }
    [Required]
    public int Text_Type_Id { get; set; }
    [Required]
    public int Question_Id { get; set; }
}
