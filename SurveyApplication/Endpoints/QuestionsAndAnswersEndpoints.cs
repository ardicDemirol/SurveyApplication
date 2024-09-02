using MediatR;
using SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class QuestionsAndAnswersEndpoints
{
    public static void MapQuestionsAndAnswersEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/Questions/GetAllSurveyQuestionsAndAnswers/Survey{id}", async (IQuestionsAndAnswers repository, IMediator mediator, int id) =>
        {
            var response = await mediator.Send(new GetAllSurveyQuestionsQueryRequest(id));
            return response;
        });
    }
}

