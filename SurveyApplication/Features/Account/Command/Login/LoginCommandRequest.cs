using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.Account.Command.Login;

public sealed record LoginCommandRequest(string email, string password) : IRequest<IActionResult>;
