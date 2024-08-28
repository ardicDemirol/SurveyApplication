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

        builder.MapGet("/surveys/getSurveyBy/{id}", async (ISurveyRepository repository, IMediator mediator, int id) =>
        {
            var response = await mediator.Send(new GetSurveyByIdQueryRequest(id));
            return Results.Ok(response);

            //var surveys = await repository.GetSurveyById<SurveyDto>(id);
            //return Results.Ok(surveys);
        });

        builder.MapGet("/surveys/getAllSurveys", async (ISurveyRepository repository, IMediator mediator) =>
        {
            var response = await mediator.Send(new GetAllSurveysQueryRequest());
            return Results.Ok(response);

            //var surveys = await repository.GetAllSurveys<ListSurveyModel>();
            //return Results.Ok(surveys);
        });

        builder.MapPost("/survey/CreateSurvey", async (ISurveyRepository surveyRepository, IMediator mediator, CreateSurveyCommandRequest createSurveyModel) =>
        {
            await mediator.Send(createSurveyModel);

            return Results.Created($"/survey/", createSurveyModel.Survey_Title);

            //await surveyRepository.CreateSurvey<SurveyDto>(newSurvey);
            //return Results.Created($"/survey/", createSurveyModel);
        });

    }

}


