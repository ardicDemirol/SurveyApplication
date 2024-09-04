using Dapper;
using SurveyApplication.Dtos.QuestionsAndAnswers;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;
public class QuestionsAndAnswersRepository(IDatabaseConnectionProvider databaseConnectionProvider) : IQuestionsAndAnswersRepository
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;

    public async Task<IEnumerable<QuestionsAndAnswersViewDto>> GetAllQuestionsAndAnswers<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string getQuestionsQuery = """
                                   SELECT question_id, question_text, answers
                                   FROM questions_and_answers
                                   WHERE survey_id = @surveyId
                                   """;

        var questionsAndAnswers = await connection.QueryAsync<QuestionsAndAnswersViewDto>(getQuestionsQuery, new { surveyId });
        return questionsAndAnswers;
    }
}
