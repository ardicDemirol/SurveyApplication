using MediatR;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;
public sealed record SaveMCACommandRequest(string Answer, int QuestionID) : IRequest;



//public sealed record SaveMCACommandRequest(string Answer, int QuestionID) : IRequest
//{
//    [Required] public string Answer { get; } = Answer;
//    [Required] public int QuestionID { get; } = QuestionID;
//}
