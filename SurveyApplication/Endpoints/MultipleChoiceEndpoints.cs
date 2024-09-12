using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;
using SurveyApplication.Validations;

namespace SurveyApplication.Endpoints;

public static class MultipleChoiceEndpoints
{
    public static void MapMultipleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/MultipleChoice/SetMaxAnswerAmount",
             [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        async (IMediator mediator, SetMCQMaxAnswerAmountRequest choice) =>
        {
            var response = await mediator.Send(choice);
            return Results.Ok($"Choice Id {response}");
        }).AddEndpointFilter<ValidatorFilter<SetMCQMaxAnswerAmountRequest>>();


        builder.MapPost("/MultipleChoice/AddChoiceToQuestion",
             [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        async (IMediator mediator, AddMCQChoiceCommandRequest choice) =>
        {
            await mediator.Send(choice);
            return Results.Ok(choice);
        }).AddEndpointFilter<ValidatorFilter<AddMCQChoiceCommandRequest>>();


        builder.MapPost("/MultipleChoice/SaveAnswer",
             [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        async (IMediator mediator, MCQSaveAnswerCommandRequest answer) =>
        {
            await mediator.Send(answer);
            return Results.Ok(answer);
        }).AddEndpointFilter<ValidatorFilter<MCQSaveAnswerCommandRequest>>();
    }
}
