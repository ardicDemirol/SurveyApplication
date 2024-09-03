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

        string getSingleChoiceQuestionQuery = """
                                              SELECT question_id, question_text, choice, answers
                                              FROM questions_and_answers
                                              WHERE survey_id = @surveyId
                                              """;


        var questionsAndAnswers = await connection.QueryAsync<QuestionsAndAnswersViewDto>(getSingleChoiceQuestionQuery, new { surveyId });

        return questionsAndAnswers;
    }
}
