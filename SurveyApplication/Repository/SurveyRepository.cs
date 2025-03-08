using Dapper;
using Hangfire;
using SurveyApplication.Dtos.SurveyDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;
using SurveyApplication.Services.Email.Interfaces;
using System.Text.Json;

namespace SurveyApplication.Repository;

public sealed class SurveyRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : ISurveyRepository
{
    #region Fields

    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
    private readonly IGarnetClient _garnetClient = garnetClient;
    private static readonly string receiverEmail = "sereb10824@obisims.com";

    private static readonly string checkCompanyExistsQuery = """
                                    SELECT company_id 
                                    FROM company 
                                    WHERE company_name = :companyName
                                    """;

    private static readonly string insertCompanyCommand = """
                               INSERT INTO company company_name
                               VALUES :companyName
                               RETURNING company_id
                               """;

    private static readonly string insertSurveyCommand = """
                              INSERT INTO surveys (survey_title, start_time, finish_time, completed_count, company_name)
                              VALUES (:name, :startTime, :finishTime, 0, :companyName)
                              """;

    private static readonly string getSurveyByIdQuery = """
                             SELECT 
                                survey_title AS SurveyTitle,
                                start_time AS StartTime,
                                finish_time AS FinishTime,
                                completed_count AS CompletedCount
                             FROM surveys
                             WHERE survey_id = :surveyId
                             """;

    private static readonly string getAllSurveyQuery = """
                            SELECT survey_id, survey_title
                            FROM surveys
                            """;

    private static readonly string existByNameQuery = """
                                  SELECT COUNT(1)
                                  FROM surveys
                                  WHERE survey_title = :surveyTitle
                                  """;

    #endregion
    public async Task CreateSurvey(SurveyDto survey)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        int? companyId = await connection.ExecuteScalarAsync<int?>(checkCompanyExistsQuery, new { companyName = survey.Company_Name });

        companyId ??= await connection.ExecuteScalarAsync<int>(insertCompanyCommand, new { companyName = survey.Company_Name });

        var parameters = new
        {
            name = survey.Survey_Title,
            startTime = survey.Start_Time,
            finishTime = survey.Finish_Time,
            companyName = survey.Company_Name
        };

        var newSurvey = await connection.ExecuteAsync(insertSurveyCommand, parameters);

        await _garnetClient.DeleteValue(CacheKeys.AllSurveysCacheKey);

        var jobId = BackgroundJob.Enqueue<IEmailService>(x => x.SendEmailAsync(
                    receiverEmail,
                    "New Survey Added",
                    $"New Survey {survey.Survey_Title} added to Su Bilgi Survey System"
                    ));

        //var jobId = BackgroundJob.Schedule<IEmailService>(x => x.SendSurveyCreatedEmail("test@gmail.com", survey.Survey_Title), TimeSpan.FromMinutes(1));
    }

    public async Task<T> GetSurveyById<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var cacheKey = $"{CacheKeys.SurveyCacheKey}{surveyId}";
        var cachedData = await garnetClient.GetValue(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        var parameters = new { surveyId };
        var result = await connection.QueryFirstOrDefaultAsync<T>(getSurveyByIdQuery, parameters);

        await _garnetClient.SetValue(cacheKey, JsonSerializer.Serialize(result));

        return result;
    }

    public async Task<IEnumerable<T>> GetAllSurveys<T>()
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var cacheKey = CacheKeys.AllSurveysCacheKey;
        var cachedData = await garnetClient.GetValue(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<List<T>>(cachedData);
        }

        var surveys = await connection.QueryAsync<T>(getAllSurveyQuery);

        await _garnetClient.SetValue(cacheKey, JsonSerializer.Serialize(surveys));

        return surveys ?? throw new ArgumentException("Survey not found");
    }

    public async Task<bool> SurveyExist(string surveyTitle)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var parameters = new { surveyTitle };

        return await connection.ExecuteScalarAsync<int>(existByNameQuery, parameters) == 1;
    }
}
