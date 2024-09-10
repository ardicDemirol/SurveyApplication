using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.Questions.Command.CreateQuestion;

public sealed record CreateQuestionCommandRequest(string Question_Text, char Question_Answer_Required, int Survey_Id, int Question_Type_Id) : IRequest<IActionResult>;
