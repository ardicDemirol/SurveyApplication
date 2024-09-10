using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

public sealed record SetMCQMaxAnswerAmountRequest(int MaxChoiceAmount, int QuestionId) : IRequest<IActionResult>;
