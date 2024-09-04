using MediatR;
using SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;
using SurveyApplication.Interfaces;
using SurveyApplication.Validation;

namespace SurveyApplication.Endpoints;

public static class SingleChoiceEndpoints
{
    public static void MapSingleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/SingleChoice/AddChoicesToQuestion", async (
            ISingleChoiceRepository repository,
            IMediator mediator,
            AddSCQChoicesCommandRequest choice) =>
        {
            await mediator.Send(choice);
        }).AddEndpointFilter<ValidatorFilter<AddSCQChoicesCommandRequest>>();


        builder.MapPost("/SingleChoice/SaveAnswer", async (
            ISingleChoiceRepository repository,
            IMediator mediator,
            SaveSCACommandRequest answer) =>
        {
            await mediator.Send(answer);

            return Results.Created($"/singlechoice/answer", answer);
        }).AddEndpointFilter<ValidatorFilter<SaveSCACommandRequest>>();

    }

}
