using MediatR;
using SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class SingleChoiceEndpoints
{
    public static void MapSingleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/SingleChoice/AddChoicesToQuestion", async (ISingleChoiceRepository repository, IMediator mediator, AddSCQChoicesCommandRequest choice) =>
        {
            await mediator.Send(choice);

            return Results.Created($"/singlechoice/", choice);
        });

        builder.MapPost("/SingleChoice/SaveAnswer", async (ISingleChoiceRepository repository, IMediator mediator, SaveSCACommandRequest answer) =>
        {
            await mediator.Send(answer);

            return Results.Created($"/singlechoice/answer", answer);
        });

        //builder.MapGet("/SingleChoice/GetAnswer", async (ISingleChoiceRepository repository, IMediator mediator, int questionId) =>
        //{
        //    var response = await mediator.Send(new GetAnswerSCQQueryRequest(questionId));

        //    return response;
        //});
    }
}
