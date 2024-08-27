using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Models;

public record CreateCompanyModel(
    [Required]
    string Company_Name
);


