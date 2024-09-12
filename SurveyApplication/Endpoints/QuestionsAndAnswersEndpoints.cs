using MediatR;
using Microsoft.AspNetCore.Authorization;
using SurveyApplication.Features.QuestionsAndAnswers.Queries.GetQuestionsAndAnswers;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class QuestionsAndAnswersEndpoints
{
    public static void MapQuestionsAndAnswersEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/QuestionsAndAnswers/GetAllSurveyQuestionsAndAnswers/Survey{id}",
            [Authorize(Roles = "admin,user")]
        async (IQuestionsAndAnswersRepository repository, IMediator mediator, int id) =>
        {
            return await mediator.Send(new GetQuestionsAndAnswersQueryRequest(id));
        });
    }
}

