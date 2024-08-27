using Dapper;
using Npgsql;
using SurveyApplication.Dtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class SingleChoiceRepository : ISingleChoiceRepository
{
    const string CONNECTION_STRING = "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;Pooling=true;";

    public async Task AddChoice(SingleChoiceQuestionDto singleChoice)
    {
        using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        string insertSingleChoice = $"INSERT INTO single_choice_questions (first_choice,second_choice,question_id) VALUES (@firstChoice,@secondChoice,@questionId)";

        var parameters = new
        {
            firstChoice = singleChoice.First_Choice,
            secondChoice = singleChoice.Second_Choice,
            questionId = singleChoice.Question_Id
        };

        await connection.ExecuteAsync(insertSingleChoice, parameters);
    }



    public async Task<bool> SaveAnswer(SingleChoiceAnswerDto answer)
    {
        using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        string checkQuestion = $"SELECT COUNT(1) FROM question WHERE question_id = @questionId AND survey_id = @surveyId";
        string checkIsMandatory = $"SELECT question_answer_required FROM question WHERE question_id = @questionId AND survey_id = @surveyId";
        string checkQuery = $"SELECT COUNT(1) FROM single_choice_answers WHERE question_id = @questionId AND survey_id = @surveyId";
        string commandText = $"INSERT INTO single_choice_answers (answer,question_id,survey_id) VALUES (@answer,@questionId,@surveyId)";

        int existingQuestionCount = await connection.ExecuteScalarAsync<int>(checkQuestion,
            new
            {
                questionId = answer.Question_Id,
                surveyId = answer.Survey_Id
            });
        if (existingQuestionCount < 1) return false;

        char charValue = await connection.ExecuteScalarAsync<char>(checkIsMandatory,
            new
            {
                questionId = answer.Question_Id,
                surveyId = answer.Survey_Id
            });

        //bool isMandatory;
        //if (charValue.ToString() == "N") isMandatory = false;
        //else isMandatory = true;



        int existingCount = await connection.ExecuteScalarAsync<int>(checkQuery,
            new
            {
                questionId = answer.Question_Id,
                surveyId = answer.Survey_Id
            });
        if (existingCount > 0) return false;

        var parameters = new
        {
            answer = answer.Answer,
            questionId = answer.Question_Id,
            surveyId = answer.Survey_Id,
        };

        await connection.ExecuteAsync(commandText, parameters);
        return true;

    }


    public async Task<T> GetAnswer<T>(int surveyId, int questionId)
    {
        using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        string commandText = $"SELECT answer FROM single_choice_answers WHERE question_id = @questionId AND survey_id = @surveyId";

        var answer = await connection.QueryFirstOrDefaultAsync<T>(commandText, new { questionId, surveyId });

        return answer ?? throw new Exception("Answer not found");
    }


}
