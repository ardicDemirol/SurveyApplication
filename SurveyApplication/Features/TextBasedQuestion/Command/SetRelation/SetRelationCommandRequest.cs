using MediatR;

namespace SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;

public sealed record SetRelationCommandRequest(int Text_Type_Id, int Question_Id) : IRequest;
