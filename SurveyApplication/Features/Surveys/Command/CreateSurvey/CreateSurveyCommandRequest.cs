using MediatR;

namespace SurveyApplication.Features.Surveys.Command.CreateSurvey;

public sealed record CreateSurveyCommandRequest(string Survey_Title, DateTime Start_Time, DateTime Finish_Time, string Company_Name) : IRequest;


//public sealed record CreateSurveyCommandRequest(string Survey_Title, DateTime Start_Time, DateTime Finish_Time, string Company_Name) : IRequest
//{
//    [Required] public string Survey_Title { get; } = Survey_Title;
//    [Required] public DateTime Start_Time { get; } = Start_Time;
//    [Required] public DateTime Finish_Time { get; } = Finish_Time;
//    [Required] public string Company_Name { get; } = Company_Name;
//}
