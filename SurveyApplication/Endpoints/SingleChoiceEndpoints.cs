using MediatR;
using SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;
using SurveyApplication.Interfaces;
using SurveyApplication.Validations;

namespace SurveyApplication.Endpoints;

public static class SingleChoiceEndpoints
{
    public static void MapSingleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/SingleChoice/AddChoicesToQuestion", async (
            ISingleChoiceRepository repository,
            IMediator mediator,
            SCQAddChoicesCommandRequest choice) =>
        {
            await mediator.Send(choice);
            return Results.Ok(choice);
        }).AddEndpointFilter<ValidatorFilter<SCQAddChoicesCommandRequest>>();


        builder.MapPost("/SingleChoice/SaveAnswer", async (
            ISingleChoiceRepository repository,
            IMediator mediator,
            SCSaveAnswerCommandRequest answer) =>
        {
            await mediator.Send(answer);
            return Results.Ok(answer);
        }).AddEndpointFilter<ValidatorFilter<SCSaveAnswerCommandRequest>>();

    }

}
