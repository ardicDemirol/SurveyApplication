using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos.TextBasedDtos;

public class TextTypesDto
{
    [Key]
    public int Text_Type_Id { get; set; }
    public string Text_Type { get; set; } = string.Empty;

}
