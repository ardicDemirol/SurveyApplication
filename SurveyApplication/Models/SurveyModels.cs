using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Models;

public record CreateSurveyModel(

    [Required] string Survey_Title,
    [Required] DateTime Start_Time,
    [Required] DateTime Finish_Time,
    [Required] string Company_Name
);

public record ListSurveyModel(
    int Survey_Id,
    string Survey_Title,
    int Completed_Count
);

