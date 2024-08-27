using Dapper;
using Npgsql;
using SurveyApplication.Dtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class SurveyRepository : ISurveyRepository
{
    const string CONNECTION_STRING = "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;Pooling=true;";
    public async Task CreateSurvey(SurveyDto survey)
    {
        using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        string checkCompanyExists = $"SELECT company_id FROM company WHERE company_name = @companyName";
        string insertCompany = $"INSERT INTO company (company_name) VALUES (@companyName) RETURNING company_id";
        string insertSurvey = $"INSERT INTO surveys (survey_title, start_time, finish_time, completed_count, company_name) VALUES (@name, @startTime, @finishTime, 0, @companyName)";

        int? companyId = await connection.ExecuteScalarAsync<int?>(checkCompanyExists, new { companyName = survey.Company_Name });

        companyId ??= await connection.ExecuteScalarAsync<int>(insertCompany, new { companyName = survey.Company_Name });

        var parameters = new
        {
            name = survey.Survey_Title,
            startTime = survey.Start_Time,
            finishTime = survey.Finish_Time,
            companyName = survey.Company_Name
        };

        await connection.ExecuteAsync(insertSurvey, parameters);
    }

    public async Task<SurveyDto> GetSurveyById(int id)
    {
        using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        string commandText = $"SELECT * FROM surveys WHERE Survey_Id = @id";
        var queryArgs = new { id };

        var surveys = await connection.QueryFirstOrDefaultAsync<SurveyDto>(commandText, queryArgs);

        return surveys ?? throw new Exception("Survey not found");
    }

    public async Task<IEnumerable<T>> GetAllSurveys<T>()
    {
        using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        string commandText = $"SELECT Survey_Id, Survey_Title, Completed_Count FROM surveys;";

        var surveys = await connection.QueryAsync<T>(commandText);


        return surveys ?? throw new Exception("Survey not found");
    }


}
