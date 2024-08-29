using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public class MultipleChoiceDto
{
    [Key]
    public int Choice_Id { get; set; }
    [Required]
    public int Max_Choice_Amount { get; set; }
    [Required]
    public int Question_Id { get; set; }

}
