using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;

public sealed record SCQAddChoicesCommandRequest(string First_Choice, string Second_Choice, int Question_Id) : IRequest<IActionResult>;