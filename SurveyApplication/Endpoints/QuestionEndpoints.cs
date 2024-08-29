using MediatR;
using SurveyApplication.Features.Questions.Command.CreateQuestion;
using SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class QuestionEndpoints
{
    public static void MapQuestionEndpoints(this IEndpointRouteBuilder builder)
    {

        builder.MapGet("/questions/GetAllSurveyQuestions/Survey{id}", async (IQuestionRepository repository, IMediator mediator, int id) =>
        {
            var response = await mediator.Send(new GetAllSurveyQuestionsQueryRequest(id));
            return Results.Ok(response);
        });


        builder.MapPost("/question/CreateQuestion", async (IQuestionRepository repository, IMediator mediator, CreateQuestionCommandRequest createQuestionModel) =>
        {
            if (createQuestionModel.Question_Answer_Required != 'Y' && createQuestionModel.Question_Answer_Required != 'N')
            {
                return Results.BadRequest("The Question_Answer_Required field must be 'Y' or 'N'.");
            }

            await mediator.Send(createQuestionModel);

            return Results.Created($"/questionId/", createQuestionModel);
        });


    }

}
