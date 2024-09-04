using MediatR;
using SurveyApplication.Features.QuestionsAndAnswers.Queries.GetQuestionsAndAnswers;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class QuestionsAndAnswersEndpoints
{
    public static void MapQuestionsAndAnswersEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/QuestionsAndAnswers/GetAllSurveyQuestionsAndAnswers/Survey{id}", async (IQuestionsAndAnswersRepository repository, IMediator mediator, int id) =>
        {
            var response = await mediator.Send(new GetQuestionsAndAnswersQueryRequest(id));
            return response;
        });
    }
}

