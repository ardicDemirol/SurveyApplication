using MediatR;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;
using SurveyApplication.Interfaces;
using SurveyApplication.Validation;

namespace SurveyApplication.Endpoints;

public static class MultipleChoiceEndpoints
{
    public static void MapMultipleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/MultipleChoice/SetMaxAnswerAmount", async (
            IMultipleChoiceRepository repository,
            IMediator mediator,
            SetMCQMaxAnswerAmountRequest choice) =>
        {
            var response = await mediator.Send(choice);
            return Results.Ok($"Choice Id {response}");
        }).AddEndpointFilter<ValidatorFilter<SetMCQMaxAnswerAmountRequest>>();


        builder.MapPost("/MultipleChoice/AddChoiceToQuestion", async (
            IMultipleChoiceRepository repository,
            IMediator mediator,
            AddMCQChoiceCommandRequest choice) =>
        {
            await mediator.Send(choice);
            return Results.Ok(choice);
        }).AddEndpointFilter<ValidatorFilter<AddMCQChoiceCommandRequest>>();


        builder.MapPost("/MultipleChoice/SaveAnswer", async (
            IMultipleChoiceRepository repository,
            IMediator mediator,
            SaveMCACommandRequest answer) =>
        {
            await mediator.Send(answer);
            return Results.Ok(answer);
        }).AddEndpointFilter<ValidatorFilter<SaveMCACommandRequest>>();
    }
}
