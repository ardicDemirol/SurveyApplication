using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.Surveys.Command.CreateQuestion;

public sealed record CreateQuestionCommandRequest(string Question_Text, char Question_Answer_Required, int Survey_Id, int Question_Type_Id) : IRequest
{
    [Required] public string Question_Text { get; } = Question_Text;
    [Required] public char Question_Answer_Required { get; } = Question_Answer_Required;
    [Required] public int Survey_Id { get; } = Survey_Id;
    [Required] public int Question_Type_Id { get; } = Question_Type_Id;
}
