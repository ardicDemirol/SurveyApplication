using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;

public sealed record TBSaveAnswerCommandRequest(string Answer, int Question_Id) : IRequest<IActionResult>;
