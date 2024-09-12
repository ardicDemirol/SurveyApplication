using MediatR;
using SurveyApplication.Features.Account.Command.Login;
using SurveyApplication.Features.Account.Command.Register;
using SurveyApplication.Validations;

namespace SurveyApplication.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/Account/Register", async (
            RegisterCommandRequest registerModel,
            IMediator mediator) =>
        {
            await mediator.Send(registerModel);
        }).AddEndpointFilter<ValidatorFilter<RegisterCommandRequest>>();


        builder.MapPost("/Account/Login", async (
            LoginCommandRequest loginModel,
            IMediator mediator) =>
        {
            var response = await mediator.Send(loginModel);
            return Results.Ok(response);

        }).AddEndpointFilter<ValidatorFilter<LoginCommandRequest>>();

    }
}
