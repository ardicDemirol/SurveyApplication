using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public class MultipleChoiceAnswersDto
{
    [Key]
    public int Answer_Id { get; set; }
    [Required]
    public string Answer { get; set; } = string.Empty;
    [Required]
    public int Question_Id { get; set; }
}
