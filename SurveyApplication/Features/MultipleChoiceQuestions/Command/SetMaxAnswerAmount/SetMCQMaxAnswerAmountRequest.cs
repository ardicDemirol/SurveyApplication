﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

public sealed record SetMCQMaxAnswerAmountRequest(int MaxChoiceAmount, int QuestionID) : IRequest
{
    [Required] public int MaxChoiceAmount { get; } = MaxChoiceAmount;
    [Required] public int QuestionID { get; } = QuestionID;
}
