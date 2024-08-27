using SurveyApplication.Dtos;
using SurveyApplication.Interfaces;
using SurveyApplication.Models;

namespace SurveyApplication.Endpoints;

public static class SingleChoiceEndpoints
{
    public static void MapSingleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/singlechoice/SaveChoices", async (ISingleChoiceRepository repository, CreateSingleChoiceAnswerModel choice) =>
        {
            SingleChoiceQuestionDto newChoice = new()
            {
                First_Choice = choice.First_Choice,
                Second_Choice = choice.Second_Choice,
                Question_Id = choice.Question_Id
            };

            await repository.AddChoice(newChoice);
            return Results.Created($"/singlechoice/", choice);
        });

        builder.MapPost("/singlechoice/SaveAnswer", async (ISingleChoiceRepository repository, SaveSingleChoiceAnswerModel answer) =>
        {
            SingleChoiceAnswerDto newAnswer = new()
            {
                Answer = answer.Answer,
                Question_Id = answer.Question_Id,
                Survey_Id = answer.Survey_Id
            };

            var result = await repository.SaveAnswer<SaveSingleChoiceAnswerModel>(newAnswer);

            //if (!result) return Results.BadRequest("The question or survey does not exist.");

            return Results.Created($"/singlechoice/answer", answer);
        });

        builder.MapGet("/singlechoice/GetAnswer", async (ISingleChoiceRepository repository, int surveyID, int questionId) =>
        {
            var result = await repository.GetAnswer<GetAnswerSingleChoiceAnswerModel>(surveyID, questionId);
            return Results.Ok(result);
        });



    }
}
