using MediatR;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;

public sealed record SaveSCACommandRequest(string Answer, int Question_Id, int Survey_Id) : IRequest;



//public sealed record SaveSCACommandRequest(string Answer, int Question_Id, int Survey_Id) : IRequest
//{
//    [Required] public string Answer { get; } = Answer;

//    [Required] public int Question_Id { get; } = Question_Id;

//    [Required] public int Survey_Id { get; } = Survey_Id;

//}
