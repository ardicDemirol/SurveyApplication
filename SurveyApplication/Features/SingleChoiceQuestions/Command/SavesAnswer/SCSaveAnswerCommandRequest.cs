using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;

public sealed record SCSaveAnswerCommandRequest(string Answer, int Question_Id) : IRequest<IActionResult>;
