using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.SingleChoiceQuestions.Queries.GetAnswer;

public sealed record GetAnswerSCQQueryResponse(
    [Required] string Answer
);
