using MediatR;
using SurveyApplication.Features.Questions.Command.CreateQuestion;
using SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;
using SurveyApplication.Interfaces;
using SurveyApplication.Validations;

namespace SurveyApplication.Endpoints;

public static class QuestionEndpoints
{
    public static void MapQuestionEndpoints(this IEndpointRouteBuilder builder)
    {

        builder.MapGet("/Questions/GetAllSurveyQuestions/Survey{id}", async (
            IQuestionRepository repository,
            IMediator mediator,
            int id) =>
        {
            return await mediator.Send(new GetAllSurveyQuestionsQueryRequest(id));
        });



        builder.MapPost("/Question/CreateQuestion", async (
            IQuestionRepository repository,
            IMediator mediator,
            CreateQuestionCommandRequest createQuestionModel) =>
        {
            await mediator.Send(createQuestionModel);
            return Results.Created($"/question/", createQuestionModel.Question_Text);
        }).AddEndpointFilter<ValidatorFilter<CreateQuestionCommandRequest>>();


    }

}
