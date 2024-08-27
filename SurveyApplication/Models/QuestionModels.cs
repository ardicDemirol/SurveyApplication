using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Models;

public record CreateQuestionModel(
    [Required] string Question_Text,
    [Required] char Question_Answer_Required,
    [Required] int Survey_Id,
    [Required] int Question_Type_Id
);

public record GetQuestionModel(
    [Required] string Question_Text
//[Required] string Question_Answer
);

