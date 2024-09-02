using MediatR;
using SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;
using SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class TextBasedEndpoints
{
    public static void MapTextBasedEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/TextBased/SetRelation", async (ITextBasedRepository repository, IMediator mediator, SetRelationCommandRequest relation) =>
        {
            await mediator.Send(relation);

            return Results.Created($"/TextBased/", relation);
        });

        builder.MapPost("/TextBased/SaveAnswer", async (ITextBasedRepository repository, IMediator mediator, SaveAnswerCommandRequest answer) =>
        {
            await mediator.Send(answer);

            return Results.Created($"/TextBased/answer", answer);
        });

        //builder.MapGet("/TextBased/GetAnswer", async (ITextBasedRepository repository, IMediator mediator, int questionId, int surveyID) =>
        //{
        //    var response = await mediator.Send(new GetAnswerSCQQueryRequest(questionId));
        //    return response;
        //});
    }
}
