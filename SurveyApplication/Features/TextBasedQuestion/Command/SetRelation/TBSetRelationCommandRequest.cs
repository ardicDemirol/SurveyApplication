using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;

public sealed record TBSetRelationCommandRequest(int Text_Type_Id, int Question_Id) : IRequest<IActionResult>;
