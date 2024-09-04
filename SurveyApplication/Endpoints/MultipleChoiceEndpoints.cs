﻿using MediatR;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;
using SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;
using SurveyApplication.Interfaces;
using SurveyApplication.Validation;

namespace SurveyApplication.Endpoints;

public static class MultipleChoiceEndpoints
{
    public static void MapMultipleChoiceEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/MultipleChoice/SetMaxChoiceAmount", async (
            IMultipleChoiceRepository repository,
            IMediator mediator,
            SetMCQMaxAnswerAmountRequest choice) =>
        {
            await mediator.Send(choice);
        }).AddEndpointFilter<ValidatorFilter<SetMCQMaxAnswerAmountRequest>>();


        builder.MapPost("/MultipleChoice/AddChoiceToQuestion", async (
            IMultipleChoiceRepository repository,
            IMediator mediator,
            AddMCQChoiceCommandRequest choice) =>
        {
            await mediator.Send(choice);
        }).AddEndpointFilter<ValidatorFilter<AddMCQChoiceCommandRequest>>();


        builder.MapPost("/MultipleChoice/SaveAnswer", async (
            IMultipleChoiceRepository repository,
            IMediator mediator,
            SaveMCACommandRequest answer) =>
        {
            await mediator.Send(answer);
        }).AddEndpointFilter<ValidatorFilter<SaveMCACommandRequest>>();
    }
}
