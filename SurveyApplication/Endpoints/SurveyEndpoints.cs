using MediatR;
using SurveyApplication.Features.Surveys.Command.CreateSurvey;
using SurveyApplication.Features.Surveys.Queries.GetAllSurveys;
using SurveyApplication.Features.Surveys.Queries.GetSurveyById;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class SurveyEndpoints
{
    public static void MapSurveyEndpoints(this IEndpointRouteBuilder builder)
    {

        builder.MapGet("/Surveys/GetSurveyById", async (ISurveyRepository repository, IMediator mediator, int surveyId) =>
        {
            var response = await mediator.Send(new GetSurveyByIdQueryRequest(surveyId));
            return Results.Ok(response);
        });

        builder.MapGet("/Surveys/GetAllSurveys", async (ISurveyRepository repository, IMediator mediator) =>
        {
            var response = await mediator.Send(new GetAllSurveysQueryRequest());
            return Results.Ok(response);
        });

        builder.MapPost("/Survey/CreateSurvey", async (ISurveyRepository surveyRepository, IMediator mediator, CreateSurveyCommandRequest createSurveyModel) =>
        {
            await mediator.Send(createSurveyModel);
            return Results.Created($"/survey/", createSurveyModel.Survey_Title);
        });

    }

}


