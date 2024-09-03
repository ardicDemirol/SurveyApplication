namespace SurveyApplication.Features.QuestionsAndAnswers.Queries.GetQuestionsAndAnswers;
public sealed record GetQuestionsAndAnswersQueryResponse(int Question_Id, string Question_Text, string Choice, string Answers);
