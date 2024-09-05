namespace SurveyApplication.Interfaces;
public interface IGarnetClient
{
    Task<string> GetValue(string key);
    Task SetValue(string key, string value);

    Task DeleteValue(string key);
}
