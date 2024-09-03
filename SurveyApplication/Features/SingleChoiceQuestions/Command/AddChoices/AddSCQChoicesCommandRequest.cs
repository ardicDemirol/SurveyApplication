using MediatR;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;

public sealed record AddSCQChoicesCommandRequest(string First_Choice, string Second_Choice, int Question_Id) : IRequest;