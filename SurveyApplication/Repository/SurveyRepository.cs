using Dapper;
using SurveyApplication.Dtos.SurveyDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;
using System.Text.Json;

namespace SurveyApplication.Repository;

public class SurveyRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : ISurveyRepository
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
    private readonly IGarnetClient _garnetClient = garnetClient;

    public async Task CreateSurvey<T>(SurveyDto survey)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string checkCompanyExists = """
                                    SELECT company_id 
                                    FROM company 
                                    WHERE company_name = @companyName
                                    """;
        string insertCompany = """
                               INSERT INTO company (company_name)
                               VALUES (@companyName)
                               RETURNING company_id
                               """;
        string insertSurvey = """
                              INSERT INTO surveys (survey_title, start_time, finish_time, completed_count, company_name)
                              VALUES (@name, @startTime, @finishTime, 0, @companyName)
                              """;

        int? companyId = await connection.ExecuteScalarAsync<int?>(checkCompanyExists, new { companyName = survey.Company_Name });

        companyId ??= await connection.ExecuteScalarAsync<int>(insertCompany, new { companyName = survey.Company_Name });

        var parameters = new
        {
            name = survey.Survey_Title,
            startTime = survey.Start_Time,
            finishTime = survey.Finish_Time,
            companyName = survey.Company_Name
        };

        var newSurvey = await connection.ExecuteAsync(insertSurvey, parameters);

        await _garnetClient.DeleteValue(CacheKeys.AllSurveysCacheKey);
    }

    public async Task<T> GetSurveyById<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        var cacheKey = $"{CacheKeys.SurveyCacheKey}{surveyId}";
        var cachedData = await garnetClient.GetValue(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        string queryText = """
                             SELECT 
                                survey_title AS SurveyTitle,
                                start_time AS StartTime,
                                finish_time AS FinishTime,
                                completed_count AS CompletedCount
                             FROM surveys
                             WHERE survey_id = @surveyId
                             """;


        var parameters = new { surveyId };
        var result = await connection.QueryFirstOrDefaultAsync<T>(queryText, parameters);

        await _garnetClient.SetValue(cacheKey, JsonSerializer.Serialize(result));

        return result ?? throw new Exception("Survey not found");
    }

    public async Task<IEnumerable<T>> GetAllSurveys<T>()
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        var cacheKey = CacheKeys.AllSurveysCacheKey;
        var cachedData = await garnetClient.GetValue(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<List<T>>(cachedData);
        }

        string commandText = """
                            SELECT survey_id, survey_title
                            FROM surveys
                            """;

        var surveys = await connection.QueryAsync<T>(commandText);

        await _garnetClient.SetValue(cacheKey, JsonSerializer.Serialize(surveys));

        return surveys ?? throw new Exception("Survey not found");
    }
}
