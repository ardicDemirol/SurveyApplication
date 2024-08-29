using MediatR;
using SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;
using SurveyApplication.Features.SingleChoiceQuestions.Queries.GetAnswer;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class ListBasedEndpoints
{
    public static void MapListBasedEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/ListBased/ListQuestion", async (ISingleChoiceRepository repository, IMediator mediator, AddSCQChoicesCommandRequest choice) =>
        {
            await mediator.Send(choice);

            return Results.Created($"/singlechoice/", choice);
        });

        builder.MapPost("/ListBased/SaveAnswer", async (ISingleChoiceRepository repository, IMediator mediator, SaveSCACommandRequest answer) =>
        {
            await mediator.Send(answer);

            return Results.Created($"/singlechoice/answer", answer);
        });

        builder.MapGet("/ListBased/GetAnswer", async (ISingleChoiceRepository repository, IMediator mediator, int questionId, int surveyID) =>
        {
            var response = await mediator.Send(new GetAnswerSCQQueryRequest(questionId, surveyID));

            return Results.Ok(response);
        });
    }
}
