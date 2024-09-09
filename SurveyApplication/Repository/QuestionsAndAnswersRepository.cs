using Dapper;
using SurveyApplication.Dtos.QuestionsAndAnswers;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;
using System.Text.Json;

namespace SurveyApplication.Repository;
public class QuestionsAndAnswersRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : IQuestionsAndAnswersRepository
{
    #region Fields

    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
    private readonly IGarnetClient _garnetClient = garnetClient;

    private static readonly string getQuestionsQuery = """
                                   SELECT question_id, question_text, answers
                                   FROM questions_and_answers
                                   WHERE survey_id = @surveyId
                                   """;

    #endregion

    public async Task<IEnumerable<QuestionsAndAnswersViewDto>> GetAllQuestionsAndAnswers<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.ConnectAndOpenConnectionAsync();

        var cacheKey = $"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}";
        var cachedData = await garnetClient.GetValue(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<List<QuestionsAndAnswersViewDto>>(cachedData);
        }

        var questionsAndAnswers = await connection.QueryAsync<QuestionsAndAnswersViewDto>(getQuestionsQuery, new { surveyId });

        await garnetClient.SetValue(cacheKey, JsonSerializer.Serialize(questionsAndAnswers));
        return questionsAndAnswers;
    }
}
