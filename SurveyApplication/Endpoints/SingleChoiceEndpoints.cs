using MediatR;
using SurveyApplication.Features.Surveys.Command.CreateSingleChoiceAnswer;
using SurveyApplication.Features.Surveys.Command.SaveSingleChoiceQuestionChoices;
using SurveyApplication.Features.Surveys.Queries.GetAnswerSingleChoiceQuestions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Endpoints;

public static class SingleChoiceEndpoints
{
    public static void MapSingleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/Singlechoice/AddChoicesToQuestion", async (ISingleChoiceRepository repository, IMediator mediator, AddSingleChoiceQuestionChoicesCommandRequest choice) =>
        {
            await mediator.Send(choice);

            return Results.Created($"/singlechoice/", choice);
        });

        builder.MapPost("/Singlechoice/SaveAnswer", async (ISingleChoiceRepository repository, IMediator mediator, SaveSingleChoiceAnswerCommandRequest answer) =>
        {
            await mediator.Send(answer);

            return Results.Created($"/singlechoice/answer", answer);
        });

        builder.MapGet("/Singlechoice/GetAnswer", async (ISingleChoiceRepository repository, IMediator mediator, int questionId, int surveyID) =>
        {
            var response = await mediator.Send(new GetAnswerSingleChoiceQuestionsQueryRequest(questionId, surveyID));

            return Results.Ok(response);
            //var result = await repository.GetAnswer<GetAnswerSingleChoiceQuestionsQueryResponse>(surveyID, questionId);
        });
    }
}
