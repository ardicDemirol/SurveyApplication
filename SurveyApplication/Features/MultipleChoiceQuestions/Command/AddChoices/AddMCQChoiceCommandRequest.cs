using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;
public sealed record AddMCQChoiceCommandRequest(string Choice, int MultipleChoiceQuestionId) : IRequest<IActionResult>;
