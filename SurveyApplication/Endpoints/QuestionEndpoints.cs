using MediatR;
using SurveyApplication.Features.Questions.Command.CreateQuestion;
using SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class QuestionEndpoints
{
    public static void MapQuestionEndpoints(this IEndpointRouteBuilder builder)
    {

        builder.MapGet("/Questions/GetAllSurveyQuestions/Survey{id}", async (IQuestionRepository repository, IMediator mediator, int id) =>
        {
            var response = await mediator.Send(new GetAllSurveyQuestionsQueryRequest(id));
            return response;
        });


        builder.MapPost("/Question/CreateQuestion", async (IQuestionRepository repository, IMediator mediator, CreateQuestionCommandRequest createQuestionModel) =>
        {
            var response = await mediator.Send(createQuestionModel);
            return response;
        });


    }

}
