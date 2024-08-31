using MediatR;

namespace SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;

public sealed record SaveAnswerCommandRequest(string Answer, int Question_Id) : IRequest;
