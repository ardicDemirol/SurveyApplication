using MediatR;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;
using SurveyApplication.Features.MultipleChoiceQuestions.Queries.GetChoices;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class MultipleChoiceEndpoints
{
    public static void MapMultipleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/MultipleChoice/SetMaxChoiceAmount", async (IMultipleChoiceRepository repository, IMediator mediator, SetMCQMaxAnswerAmountRequest choice) =>
        {
            await mediator.Send(choice);
        });

        builder.MapPost("/MultipleChoice/AddChoicesToQuestion", async (IMultipleChoiceRepository repository, IMediator mediator, AddMCQChoicesCommandRequest choice) =>
        {
            await mediator.Send(choice);

            return Results.Created($"/Multiplechoice/", choice);
        });

        builder.MapPost("/MultipleChoice/SaveAnswer", async (IMultipleChoiceRepository repository, IMediator mediator, SaveMCACommandRequest answer) =>
        {
            await mediator.Send(answer);
        });

        ///
        builder.MapGet("/MultipleChoice/GetChoices", async (IMultipleChoiceRepository repository, IMediator mediator, int questionId) =>
        {
            var response = await mediator.Send(new GetChoicesMCQQueryRequest(questionId));

            return Results.Ok(response);
        });
        ///
    }
}
