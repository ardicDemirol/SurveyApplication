using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.Surveys.Command.CreateSingleChoiceAnswer;

public sealed record SaveSingleChoiceAnswerCommandRequest(string Answer, int Question_Id, int Survey_Id) : IRequest
{
    [Required] public string Answer { get; } = Answer;

    [Required] public int Question_Id { get; } = Question_Id;

    [Required] public int Survey_Id { get; } = Survey_Id;

}
