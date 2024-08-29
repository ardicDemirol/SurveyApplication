using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos.ListBasedDtos;

public class ListBasedQuestions
{
    [Key]
    public int List_Id { get; set; }
    [Required]
    public int Question_Id { get; set; }
}
