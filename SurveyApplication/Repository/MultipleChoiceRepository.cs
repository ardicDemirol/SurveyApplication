using Dapper;
using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Extensions;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Repository
{
    public class MultipleChoiceRepository(IDatabaseConnectionProvider databaseConnectionProvider, IGarnetClient garnetClient) : IMultipleChoiceRepository
    {
        private readonly IDatabaseConnectionProvider _databaseConnectionProvider = databaseConnectionProvider;
        private readonly IGarnetClient _garnetClient = garnetClient;

        public async Task<int> SetMaxAnswerAmount(MultipleChoiceDto question)
        {
            using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

            string checkQuestionQuery = """
                    SELECT COUNT(1)
                    FROM question
                    WHERE question_id = @questionId
                    AND question_type_id = 2;
                    """;

            string checkAnswerQuery = """
                    SELECT COUNT(1)
                    FROM multiple_choice_questions
                    WHERE question_id = @questionId
                    """;

            string insertCommand = """
                         INSERT INTO multiple_choice_questions (max_choice_amount,question_id)
                         VALUES (@maxSize,@questionId)
                         RETURNING choice_id;
                         """;


            int matchedQuestionCount = await connection.ExecuteScalarAsync<int>(checkQuestionQuery, new { questionId = question.Question_Id });
            int matchedAnswerCount = await connection.ExecuteScalarAsync<int>(checkAnswerQuery, new { questionId = question.Question_Id });

            if (matchedQuestionCount < 1) return -1;
            if (matchedAnswerCount > 0) return -1;


            var parameters = new
            {
                maxSize = question.Max_Choice_Amount,
                questionId = question.Question_Id
            };

            return await connection.ExecuteScalarAsync<int>(insertCommand, parameters);
        }

        public async Task AddChoice(MultipleOtherChoicesDto newChoice)
        {
            using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

            string insertCommand = """
                                    INSERT INTO other_choices (choice,multiple_choice_questions_id) 
                                    VALUES (@choice,@multipleChoiceQuestionsId)
                                    """;

            var parameters = new
            {
                choice = newChoice.Choice,
                multipleChoiceQuestionsId = newChoice.Multiple_Choice_Question_Id
            };

            await connection.ExecuteAsync(insertCommand, parameters);



            string questionIdQuery = """
                                 SELECT question_id
                                 FROM multiple_choice_questions
                                 WHERE choice_id = @multiple_Choice_Question_Id
                                 """;
            int questionId = await connection.ExecuteScalarAsync<int>(questionIdQuery, new { newChoice.Multiple_Choice_Question_Id });

            string surveyIdQuery = """
                                 SELECT survey_id
                                 FROM question
                                 WHERE question_id = @questionId
                                 """;
            int surveyId = await connection.ExecuteScalarAsync<int>(surveyIdQuery, new { questionId });

            await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{surveyId}");
            await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}");

        }

        public async Task SaveAnswer(MultipleChoiceAnswersDto answerModel)
        {
            using var connection = await _databaseConnectionProvider.GetOpenConnectionAsync();

            string insertCommand = """
                                   INSERT INTO multiple_choice_answers (answer,question_id) 
                                   VALUES (@answer,@questionId)
                                   """;

            string checkMaxAnswerQuery = """
                                         SELECT MAX(max_choice_amount) AS max_choice_amount
                                         FROM multiple_choice_questions
                                         WHERE question_id = @questionId
                                         """;


            string checkAnswerQuery = """
                                      SELECT COUNT(1)
                                      FROM multiple_choice_answers
                                      WHERE question_id = @questionId
                                      """;

            string createChoicesViewQuery = """
                                     SELECT EXISTS (
                                         SELECT 1
                                         FROM other_choices oc
                                         JOIN multiple_choice_questions mcq ON oc.multiple_choice_questions_id = mcq.choice_id
                                         WHERE oc.choice = @answer
                                           AND mcq.question_id = @questionId
                                     )
                                     """;



            var parameters = new
            {
                answer = answerModel.Answer,
                questionId = answerModel.Question_Id
            };

            bool exists = connection.ExecuteScalar<bool>(createChoicesViewQuery, parameters);

            if (!exists) throw new Exception("Choice does not exist");

            int matchedAnswerCount = await connection.ExecuteScalarAsync<int>(checkAnswerQuery, new { questionId = answerModel.Question_Id });
            int maxAnswerAmount = await connection.ExecuteScalarAsync<int>(checkMaxAnswerQuery, new { questionId = answerModel.Question_Id });

            if (matchedAnswerCount >= maxAnswerAmount) throw new Exception("You have reached the maximum number of answers");

            string surveyIdQuery = """
                                 SELECT survey_id
                                 FROM question
                                 WHERE question_id = @question_Id
                                 """;
            int surveyId = await connection.ExecuteScalarAsync<int>(surveyIdQuery, new { answerModel.Question_Id });

            await _garnetClient.DeleteValue($"{CacheKeys.SurveyQuestionsCacheKey}{surveyId}");
            await _garnetClient.DeleteValue($"{CacheKeys.AllSurveyQuestionsAndAnswersCacheKey}{surveyId}");

            await connection.ExecuteAsync(insertCommand, parameters);
        }
    }
}

