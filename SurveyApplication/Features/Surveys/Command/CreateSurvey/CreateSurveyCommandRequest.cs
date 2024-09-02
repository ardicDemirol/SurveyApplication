using MediatR;

namespace SurveyApplication.Features.Surveys.Command.CreateSurvey;

public sealed record CreateSurveyCommandRequest(string Survey_Title, DateTime Finish_Time, string Company_Name) : IRequest;
