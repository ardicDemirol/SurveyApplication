using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SurveyApplication.Features.Surveys.Command.CreateSurvey;
using SurveyApplication.Features.Surveys.Queries.GetAllSurveys;
using SurveyApplication.Features.Surveys.Queries.GetSurveyById;
using SurveyApplication.Validations;

namespace SurveyApplication.Endpoints;

public static class SurveyEndpoints
{
    public static void MapSurveyEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/Surveys/CreateSurvey",
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        async (IMediator mediator, CreateSurveyCommandRequest createSurveyModel, CancellationToken token) =>
        {
            await mediator.Send(createSurveyModel);
            return Results.Created($"/survey/", createSurveyModel.Survey_Title);
        }).AddEndpointFilter<ValidatorFilter<CreateSurveyCommandRequest>>();


        builder.MapGet("/Surveys/GetSurveyById",
            async (IMediator mediator, int surveyId) =>
        {
            var response = await mediator.Send(new GetSurveyByIdQueryRequest(surveyId));
            return Results.Ok(response);
        });


        builder.MapGet("/Surveys/GetAllSurveys",
            async (IMediator mediator) =>
        {
            var response = await mediator.Send(new GetAllSurveysQueryRequest());
            return Results.Ok(response);
        });

    }

}





