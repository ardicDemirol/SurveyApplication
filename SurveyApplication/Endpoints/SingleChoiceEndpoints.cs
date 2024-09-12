using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;
using SurveyApplication.Validations;

namespace SurveyApplication.Endpoints;

public static class SingleChoiceEndpoints
{
    public static void MapSingleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/SingleChoice/AddChoicesToQuestion",
             [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        async (IMediator mediator, SCQAddChoicesCommandRequest choice) =>
        {
            await mediator.Send(choice);
            return Results.Ok(choice);
        }).AddEndpointFilter<ValidatorFilter<SCQAddChoicesCommandRequest>>();


        builder.MapPost("/SingleChoice/SaveAnswer",
             [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        async (IMediator mediator, SCSaveAnswerCommandRequest answer) =>
        {
            await mediator.Send(answer);
            return Results.Ok(answer);
        }).AddEndpointFilter<ValidatorFilter<SCSaveAnswerCommandRequest>>();

    }

}
