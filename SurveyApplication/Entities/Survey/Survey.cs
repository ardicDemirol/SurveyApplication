using SurveyApplication.Dtos.SurveyDtos;

namespace SurveyApplication.Entities.Survey;
public sealed class Survey : Entity
{
    public string Survey_Title { get; }
    public DateTime Start_Time { get; }
    public DateTime Finish_Time { get; }
    public int Completed_Count { get; }
    public string Company_Name { get; }

    private Survey(
        int id,
        string surveyTitle,
        DateTime startTime,
        DateTime finishTime,
        int completedCount,
        string companyName)
        : base(id)
    {
        Survey_Title = surveyTitle;
        Start_Time = startTime;
        Finish_Time = finishTime;
        Completed_Count = completedCount;
        Company_Name = companyName;
    }

    public static Survey Create(
        int id,
        string surveyTitle,
        DateTime startTime,
        DateTime finishTime,
        int completedCount,
        string companyName)
    {
        return new Survey(
            id,
            surveyTitle,
            startTime,
            finishTime,
            completedCount,
            companyName);
    }

    public static Survey FromDto(SurveyDto surveyDto)
    {
        return new Survey(
            surveyDto.Survey_Id,
            surveyDto.Survey_Title,
            surveyDto.Start_Time,
            surveyDto.Finish_Time,
            surveyDto.Completed_Count,
            surveyDto.Company_Name);
    }
}
