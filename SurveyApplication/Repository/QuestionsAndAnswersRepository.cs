using Dapper;
using SurveyApplication.Dtos.QuestionDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;
public class QuestionsAndAnswersRepository(IDatabaseConnectionProvider databaseConnectionProvider) : IQuestionsAndAnswers
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;

    public async Task<IEnumerable<QuestionChoicesViewDto>> GetAllQuestionsAndAnswers<T>(int surveyId)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string getSingleChoiceQuestionQuery = """
                                              SELECT question_id, question_text,choice
                                              FROM question_choices_view
                                              WHERE survey_id = @surveyId
                                              """;


        var singleChoiceQuestion = await connection.QueryAsync<QuestionChoicesViewDto>(getSingleChoiceQuestionQuery, new { surveyId });

        return singleChoiceQuestion;
    }
}
