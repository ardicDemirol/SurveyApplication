using MediatR;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;
public sealed record AddMCQChoicesCommandRequest(string Choice, int MultipleChoiceQuestionId) : IRequest;


//public sealed record AddMCQChoicesCommandRequest(string Choice, int MultipleChoiceQuestionId) : IRequest
//{
//    [Required] public string Choice { get; } = Choice;
//    [Required] public int Multiple_Choice_Question_Id { get; } = MultipleChoiceQuestionId;
//}
