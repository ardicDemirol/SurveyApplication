using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;
using SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;
using SurveyApplication.Validations;

namespace SurveyApplication.Endpoints;

public static class TextBasedEndpoints
{
    public static void MapTextBasedEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/TextBased/SetRelation",
             [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        async (IMediator mediator, TBSetRelationCommandRequest relation) =>
        {
            await mediator.Send(relation);
            return Results.Created($"/TextBased/", relation);
        }).AddEndpointFilter<ValidatorFilter<TBSetRelationCommandRequest>>();


        builder.MapPost("/TextBased/SaveAnswer",
             [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        async (IMediator mediator, TBSaveAnswerCommandRequest answer) =>
        {
            await mediator.Send(answer);
            return Results.Created($"/TextBased/answer", answer);
        }).AddEndpointFilter<ValidatorFilter<TBSaveAnswerCommandRequest>>();

    }
}
