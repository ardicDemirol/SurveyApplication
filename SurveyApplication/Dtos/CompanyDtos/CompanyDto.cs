using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Dtos.CompanyDtos;

public class CompanyDto
{
    [Key]
    public int Company_Id { get; set; }
    [Required]
    public string Company_Name { get; set; } = string.Empty;

}
