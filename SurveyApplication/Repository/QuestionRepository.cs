using Dapper;
using SurveyApplication.Dtos.QuestionDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;
public class QuestionRepository(IDatabaseConnectionProvider databaseConnectionProvider) : IQuestionRepository
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;

    public async Task CreateQuestion(QuestionDto question)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        int newQuestionOrderId = await connection.GetNextIdAsync("question", "question_order", "survey_id", question.Survey_Id) + 1;

        string insertQuestion = """
                                INSERT INTO question (question_text, question_order, question_answer_required,survey_id,question_type_id)
                                VALUES (@questionText,@questionOrder,@questionAnswerRequired,@surveyId,@questionTypeId)
                                """;

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

    public async Task<IEnumerable<SingleChoiceQuestionChoicesViewDto>> GetAllQuestions<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string checkSurveyQuery = """
                                  SELECT COUNT(1)
                                  FROM surveys 
                                  WHERE survey_id = @surveyId
                                  """;

        int existingSurveyCount = await connection.ExecuteScalarAsync<int>(checkSurveyQuery, new { surveyId });

        if (existingSurveyCount < 1) throw new Exception("No such survey was found");

        string getSingleChoiceQuestionQuery = """
                                                 SELECT question_id, question_text,choice
                                                 FROM question_choices_view
                                                 WHERE survey_id = @surveyId
                                                 """;

        var singleChoiceQuestion = await connection.QueryAsync<SingleChoiceQuestionChoicesViewDto>(getSingleChoiceQuestionQuery, new { surveyId });

        return singleChoiceQuestion;

    }
}
