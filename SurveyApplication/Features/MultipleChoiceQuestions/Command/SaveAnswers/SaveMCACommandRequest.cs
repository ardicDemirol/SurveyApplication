using MediatR;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;
public sealed record SaveMCACommandRequest(string Answer, int QuestionId) : IRequest;
