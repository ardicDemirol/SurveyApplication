using Dapper;
using SurveyApplication.Dtos.SingleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository;

public class SingleChoiceRepository(IDatabaseConnectionProvider databaseConnectionProvider) : ISingleChoiceRepository
{
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;

    public async Task AddChoice(SingleChoiceQuestionDto singleChoice)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string insertCommand = """
                              INSERT INTO single_choice_questions (first_choice,second_choice,question_id) 
                              VALUES (@firstChoice,@secondChoice,@questionId)
                              """;

        var parameters = new
        {
            firstChoice = singleChoice.First_Choice,
            secondChoice = singleChoice.Second_Choice,
            questionId = singleChoice.Question_Id
        };

        await connection.ExecuteAsync(insertCommand, parameters);
    }



    public async Task SaveAnswer(SingleChoiceAnswerDto answer)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string checkQuestionQuery = """ 
                                    SELECT COUNT(1)
                                    FROM question 
                                    WHERE question_id = @questionId     
                                        AND survey_id = @surveyId
                                    """;

        string getChoicesQuery = """
                                 SELECT first_choice, second_choice 
                                 FROM single_choice_questions 
                                 WHERE question_id = @questionId
                                 """;

        string checkIsMandatory = """ 
                                  SELECT question_answer_required 
                                  FROM question 
                                  WHERE question_id = @questionId 
                                    AND survey_id = @surveyId
                                  """;

        string checkQuery = """
                            SELECT COUNT(1)
                            FROM single_choice_answers 
                            WHERE question_id = @questionId 
                                AND survey_id = @surveyId
                            """;

        string insertAnswerCommand = """
                            INSERT INTO single_choice_answers (answer,question_id,survey_id) 
                            VALUES (@answer,@questionId,@surveyId)
                            """;

        int existingQuestionCount = await connection.ExecuteScalarAsync<int>(checkQuestionQuery,
            new
            {
                questionId = answer.Question_Id,
                surveyId = answer.Survey_Id
            });

        if (existingQuestionCount < 1) throw new Exception("No such question was found");

        //char charValue = await connection.ExecuteScalarAsync<char>(checkIsMandatory,
        //    new
        //    {
        //        questionId = answer.Question_Id,
        //        surveyId = answer.Survey_Id
        //    });
        //bool isMandatory;
        //if (charValue.ToString() == "N") isMandatory = false;
        //else isMandatory = true;

        var validChoices = connection.Query(getChoicesQuery, new { questionId = answer.Question_Id })
                                 .SelectMany(row => new List<string> { row.first_choice, row.second_choice })
                                 .ToList();

        string userAnswer = answer.Answer;

        if (!validChoices.Contains(userAnswer)) throw new Exception("Answer not found in the choices");

        int existingCount = await connection.ExecuteScalarAsync<int>(checkQuery,
            new
            {
                questionId = answer.Question_Id,
                surveyId = answer.Survey_Id
            });

        if (existingCount > 0) throw new Exception("You replied this question before");

        var parameters = new
        {
            answer = answer.Answer,
            questionId = answer.Question_Id,
            surveyId = answer.Survey_Id,
        };

        await connection.ExecuteAsync(insertAnswerCommand, parameters);
    }


    public async Task<T> GetAnswer<T>(int questionId, int surveyId)
    {
        using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

        string commandText = """
            SELECT answer
            FROM single_choice_answers
            WHERE question_id = @questionId 
            AND survey_id = @surveyId
            """;

        var answer = await connection.QueryFirstOrDefaultAsync<T>(commandText, new { questionId, surveyId });

        return answer ?? throw new Exception("Answer not found");
    }

}
