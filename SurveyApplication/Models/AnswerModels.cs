using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Models;

public record CreateSingleChoiceAnswerModel(
    [Required] string First_Choice,
    [Required] string Second_Choice,
    [Required] int Question_Id
);

public record SaveSingleChoiceAnswerModel(
    [Required] string Answer,
    [Required] int Question_Id,
    [Required] int Survey_Id
);

public record GetAnswerSingleChoiceAnswerModel(
    [Required] string Answer
);

