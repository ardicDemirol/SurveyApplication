using SurveyApplication.Dtos;

namespace SurveyApplication.Interfaces;

public interface ISurveyRepository
{
    Task CreateSurvey(SurveyDto survey);
    Task<SurveyDto> GetSurveyById(int id);
    Task<IEnumerable<T>> GetAllSurveys<T>();

}
