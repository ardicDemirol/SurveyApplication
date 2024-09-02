using MediatR;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;
public sealed record AddMCQChoicesCommandRequest(string Choice, int MultipleChoiceQuestionId) : IRequest;
