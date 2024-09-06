using Dapper;
using SurveyApplication.Dtos.QuestionDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;
using System.Text.Json;

namespace SurveyApplication.Repository;
public class QuestionRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : IQuestionRepository
{
    #region Fields

    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
    private readonly IGarnetClient _garnetClient = garnetClient;

    private static readonly string insertQuestion = """
                                INSERT INTO question (question_text, question_order, question_answer_required,survey_id,question_type_id)
                                VALUES (@questionText,@questionOrder,@questionAnswerRequired,@surveyId,@questionTypeId)
                                """;

    private static readonly string checkSurveyQuery = """
                                  SELECT COUNT(1)
                                  FROM surveys 
                                  WHERE survey_id = @surveyId
                                  """;

    private static readonly string getQuestionsWithChoicesQuery = """
                                              SELECT question_id, question_text,choice
                                              FROM question_choices_view
                                              WHERE survey_id = @surveyId
                                              """;

    private static readonly string getQuestionsTextBasedQuery = """
                                              SELECT question_id, question_text
                                              FROM question
                                              WHERE survey_id = @surveyId
                                              AND question_type_id = 4
                                              """;

    #endregion

    public async Task CreateQuestion(QuestionDto question)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        int newQuestionOrderId = await connection.GetNextIdAsync("question", "question_order", "survey_id", question.Survey_Id) + 1;


        var parameters = new
        {
            questionText = question.Question_Text,
            questionOrder = newQuestionOrderId,
            questionAnswerRequired = question.Question_Answer_Required,
            surveyId = question.Survey_Id,
            questionTypeId = question.Question_Type_Id
        };

        await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{question.Survey_Id}");
        await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{question.Survey_Id}");
        await connection.ExecuteAsync(insertQuestion, parameters);
    }

    public async Task<IEnumerable<QuestionChoicesViewDto>> GetAllSurveyQuestions<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        var cacheKey = $"{CacheKeys.SurveyQuestionsCacheKey}{surveyId}";
        var cachedData = await garnetClient.GetValue(cacheKey);


        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<List<QuestionChoicesViewDto>>(cachedData);
        }

        int existingSurveyCount = await connection.ExecuteScalarAsync<int>(checkSurveyQuery, new { surveyId });

        if (existingSurveyCount < 1) throw new ArgumentException("No such survey was found");

        var choices = await connection.QueryAsync<QuestionChoicesViewDto>(getQuestionsWithChoicesQuery, new { surveyId });

        var choicesTextBased = await connection.QueryAsync<QuestionChoicesViewDto>(getQuestionsTextBasedQuery, new { surveyId });

        var allChoices = choices
            .Concat(choicesTextBased)
            .OrderBy(c => c.Question_Id);

        await _garnetClient.SetValue(cacheKey, JsonSerializer.Serialize(allChoices));

        return allChoices;

    }
}
