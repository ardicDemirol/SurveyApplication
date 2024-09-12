using SurveyApplication.Dtos.SurveyDtos;
using SurveyApplication.Entities.Survey;

namespace SurveyApplication.Mappers;
public static class SurveyMapper
{
    public static SurveyDto ToDto(this Survey survey)
    {
        return SurveyDto.Create(
            survey.Survey_Title,
            survey.Start_Time,
            survey.Finish_Time,
            survey.Completed_Count,
            survey.Company_Name);
    }
}
