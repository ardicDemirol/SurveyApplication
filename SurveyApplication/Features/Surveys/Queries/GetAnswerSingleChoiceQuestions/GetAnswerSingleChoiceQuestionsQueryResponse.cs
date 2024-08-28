using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.Surveys.Queries.GetAnswerSingleChoiceQuestions;

public sealed record GetAnswerSingleChoiceQuestionsQueryResponse(
    [Required] string Answer
);
