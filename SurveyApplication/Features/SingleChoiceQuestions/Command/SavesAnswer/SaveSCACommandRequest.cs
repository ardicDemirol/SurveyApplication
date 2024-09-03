using MediatR;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;

public sealed record SaveSCACommandRequest(string Answer, int Question_Id, int Survey_Id) : IRequest;
