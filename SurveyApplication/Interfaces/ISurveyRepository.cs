using SurveyApplication.Dtos.SurveyDtos;

namespace SurveyApplication.Interfaces;

public interface ISurveyRepository
{
    Task CreateSurvey(SurveyDto survey);
    Task<T> GetSurveyById<T>(int id);
    Task<IEnumerable<T>> GetAllSurveys<T>();
    Task<bool> SurveyExist(string surveyTitle);
}
