using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;
public sealed record MCQSaveAnswerCommandRequest(string Answer, int QuestionId) : IRequest<IActionResult>;
