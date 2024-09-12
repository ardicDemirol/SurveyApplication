using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.Account.Command.Register;

public sealed record RegisterCommandRequest(string email, string password, string role) : IRequest<IActionResult>;
