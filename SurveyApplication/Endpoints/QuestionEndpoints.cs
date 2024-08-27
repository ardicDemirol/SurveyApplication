using SurveyApplication.Dtos;
using SurveyApplication.Interfaces;
using SurveyApplication.Models;

namespace SurveyApplication.Endpoints;

public static class QuestionEndpoints
{
    public static void MapQuestionEndpoints(this IEndpointRouteBuilder builder)
    {

        builder.MapGet("/questions/GetAllQuestions/Survey{id}", async (IQuestionRepository repository, int id) =>
        {
            var question = await repository.GetAllQuestions<GetQuestionModel>(id);
            return Results.Ok(question);
        });


        builder.MapPost("/question/CreateQuestion", async (IQuestionRepository repository, CreateQuestionModel questionDto) =>
        {
            if (questionDto.Question_Answer_Required != 'Y' && questionDto.Question_Answer_Required != 'N')
            {
                return Results.BadRequest("The Question_Answer_Required field must be 'Y' or 'N'.");
            }

            QuestionDto newQuestion = new()
            {
                Question_Text = questionDto.Question_Text,
                Question_Answer_Required = questionDto.Question_Answer_Required,
                Survey_Id = questionDto.Survey_Id,
                Question_Type_Id = questionDto.Question_Type_Id
            };

            await repository.CreateQuestion(newQuestion);
            return Results.Created($"/question/", questionDto);

        });


    }

}
