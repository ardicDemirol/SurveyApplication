using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;

public sealed record AddSCQChoicesCommandRequest(string First_Choice, string Second_Choice, int Question_Id) : IRequest
{
    [Required] public string First_Choice { get; } = First_Choice;
    [Required] public string Second_Choice { get; } = Second_Choice;
    [Required] public int Question_Id { get; } = Question_Id;
}
