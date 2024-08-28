using SurveyApplication.Dtos;

namespace SurveyApplication.Interfaces;

public interface ISurveyRepository
{
    Task CreateSurvey<T>(SurveyDto survey);
    Task<T> GetSurveyById<T>(int id);
    Task<IEnumerable<T>> GetAllSurveys<T>();

}
