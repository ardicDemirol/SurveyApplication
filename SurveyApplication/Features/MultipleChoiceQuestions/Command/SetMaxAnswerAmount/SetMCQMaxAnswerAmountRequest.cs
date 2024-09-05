using MediatR;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

public sealed record SetMCQMaxAnswerAmountRequest(int MaxChoiceAmount, int QuestionID) : IRequest<int>;
