using SurveyApplication.Dtos;
using SurveyApplication.Interfaces;
using SurveyApplication.Models;

namespace SurveyApplication.Endpoints;
public static class SurveyEndpoints
{
    public static void MapSurveyEndpoints(this IEndpointRouteBuilder builder)
    {

        builder.MapGet("/surveys/{id}", async (ISurveyRepository repository, int id) =>
        {
            var surveys = await repository.GetSurveyById(id);
            return Results.Ok(surveys);
        });

        builder.MapGet("/surveys", async (ISurveyRepository repository) =>
        {
            var surveys = await repository.GetAllSurveys<ListSurveyModel>();
            return Results.Ok(surveys);
        });


        builder.MapPost("/survey", async (ISurveyRepository surveyRepository, CreateSurveyModel surveyDto) =>
        {
            SurveyDto newSurvey = new()
            {
                Survey_Title = surveyDto.Survey_Title,
                Start_Time = surveyDto.Start_Time,
                Finish_Time = surveyDto.Finish_Time,
                Company_Name = surveyDto.Company_Name
            };

            await surveyRepository.CreateSurvey(newSurvey);
            return Results.Created($"/survey/", surveyDto);
        });

    }

}
