namespace SurveyApplication.Features.QuestionsAndAnswers.Queries.GetQuestionsAndAnswers;
public sealed record GetQuestionsAndAnswersQueryResponse(int question_id, string question_text, string answers);
