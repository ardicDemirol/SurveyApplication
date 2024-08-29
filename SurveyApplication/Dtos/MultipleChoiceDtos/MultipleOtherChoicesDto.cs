using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public class MultipleOtherChoicesDto
{
    [Key]
    public int Other_Choice_Id { get; set; }
    [Required]
    public string Choice { get; set; } = string.Empty;
    [Required]
    public int Multiple_Choice_Question_Id { get; set; } = 0;
}
