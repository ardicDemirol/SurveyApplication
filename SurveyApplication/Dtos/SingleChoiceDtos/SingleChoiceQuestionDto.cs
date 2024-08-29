using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos.SingleChoiceDtos;
public class SingleChoiceQuestionDto
{
    [Key]
    public int Choice_Id { get; set; }
    [Required]
    public string First_Choice { get; set; } = string.Empty;
    [Required]
    public string Second_Choice { get; set; } = string.Empty;
    [Required]
    public int Question_Id { get; set; }
}
