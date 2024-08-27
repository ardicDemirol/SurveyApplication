using Dapper;
using Npgsql;
using SurveyApplication.Dtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;
public class QuestionRepository : IQuestionRepository
{
    const string CONNECTION_STRING = "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;Pooling=true;";

    public async Task CreateQuestion(QuestionDto question)
    {
        using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        int newQuestionOrderId = await connection.GetNextIdAsync("question", "question_order", "survey_id", question.Survey_Id) + 1;

        string insertQuestion = $"INSERT INTO question (question_text, question_order, question_answer_required,survey_id,question_type_id) " +
            $"VALUES (@questionText,@questionOrder,@questionAnswerRequired,@surveyId,@questionTypeId)";

        var parameters = new
        {
            questionText = question.Question_Text,
            questionOrder = newQuestionOrderId,
            questionAnswerRequired = question.Question_Answer_Required,
            surveyId = question.Survey_Id,
            questionTypeId = question.Question_Type_Id
        };

        await connection.ExecuteAsync(insertQuestion, parameters);

    }

    public async Task<IEnumerable<T>> GetAllQuestions<T>(int surveyId)
    {
        using var connection = new NpgsqlConnection(CONNECTION_STRING);
        await connection.OpenAsync();

        string commandText = $"SELECT (question_text) FROM question WHERE survey_id = @surveyId";

        var questions = await connection.QueryAsync<T>(commandText, new { surveyId });
        return questions;

    }

    //public async Task<QuestionDto> GetQuestionById(int id)
    //{
    //    throw new NotImplementedException();
    //}
}
