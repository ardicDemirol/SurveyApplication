using MediatR;
using SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;
using SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;
using SurveyApplication.Interfaces;
using SurveyApplication.Validation;

namespace SurveyApplication.Endpoints;

public static class TextBasedEndpoints
{
    public static void MapTextBasedEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/TextBased/SetRelation", async (
            ITextBasedRepository repository,
            IMediator mediator,
            SetRelationCommandRequest relation) =>
        {
            await mediator.Send(relation);

            return Results.Created($"/TextBased/", relation);
        }).AddEndpointFilter<ValidatorFilter<SetRelationCommandRequest>>();



        builder.MapPost("/TextBased/SaveAnswer", async (
            ITextBasedRepository repository,
            IMediator mediator,
            SaveAnswerCommandRequest answer) =>
        {
            await mediator.Send(answer);

            return Results.Created($"/TextBased/answer", answer);
        }).AddEndpointFilter<ValidatorFilter<SaveAnswerCommandRequest>>();

    }
}
