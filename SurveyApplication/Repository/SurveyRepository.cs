using Dapper;
using SurveyApplication.Dtos.SurveyDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class SurveyRepository(IDatabaseConnectionProvider databaseConnectionProvider) : ISurveyRepository
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;

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


    }

    public async Task<T> GetSurveyById<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string commandText = """
                             SELECT survey_id AS SurveyId, survey_title AS SurveyTitle, completed_count AS CompletedCount
                             FROM surveys
                             WHERE survey_id = @surveyId
                             """;



        var parameters = new { surveyId };
        var result = await connection.QueryFirstOrDefaultAsync<T>(commandText, parameters);
        return result ?? throw new Exception("Survey not found");
    }

    public async Task<IEnumerable<T>> GetAllSurveys<T>()
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string commandText = """
                            SELECT Survey_Id, Survey_Title, Completed_Count 
                            FROM surveys
                            """;

        var surveys = await connection.QueryAsync<T>(commandText);
        return surveys ?? throw new Exception("Survey not found");
    }


}
