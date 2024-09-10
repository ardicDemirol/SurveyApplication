namespace SurveyApplication.Dtos.SurveyDtos;

public sealed record SurveyDto
{
    public int Survey_Id { get; }
    public string Survey_Title { get; }
    public DateTime Start_Time { get; }
    public DateTime Finish_Time { get; }
    public int Completed_Count { get; }
    public string Company_Name { get; }

    private SurveyDto(string surveyTitle, DateTime startTime, DateTime finishTime, int completedCount, string companyName)
    {
        Survey_Title = surveyTitle;
        Start_Time = startTime;
        Finish_Time = finishTime;
        Completed_Count = completedCount;
        Company_Name = companyName;
    }

    public static SurveyDto Create(string surveyTitle, DateTime startTime, DateTime finishTime, int completedCount, string companyName)
    {
        return new SurveyDto(surveyTitle, startTime, finishTime, completedCount, companyName);
    }
    public static SurveyDto Empty() => new(
        surveyTitle: string.Empty,
        startTime: default,
        finishTime: default,
        completedCount: 0,
        companyName: string.Empty
    );

}

