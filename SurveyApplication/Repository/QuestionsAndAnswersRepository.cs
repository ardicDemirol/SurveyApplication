using Dapper;
using SurveyApplication.Dtos.QuestionsAndAnswers;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;
using System.Text.Json;

namespace SurveyApplication.Repository;
public class QuestionsAndAnswersRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : IQuestionsAndAnswersRepository
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
    private readonly IGarnetClient _garnetClient = garnetClient;

    public async Task<IEnumerable<QuestionsAndAnswersViewDto>> GetAllQuestionsAndAnswers<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        var cacheKey = $"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}";
        var cachedData = await garnetClient.GetValue(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<List<QuestionsAndAnswersViewDto>>(cachedData);
        }


        string getQuestionsQuery = """
                                   SELECT question_id, question_text, answers
                                   FROM questions_and_answers
                                   WHERE survey_id = @surveyId
                                   """;

        var questionsAndAnswers = await connection.QueryAsync<QuestionsAndAnswersViewDto>(getQuestionsQuery, new { surveyId });

        await garnetClient.SetValue(cacheKey, JsonSerializer.Serialize(questionsAndAnswers));
        return questionsAndAnswers;
    }
}
