namespace SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;

public sealed record GetAllSurveyQuestionsQueryResponse(int Question_Id, string Question_Text, string First_Choice, string Second_Choice);
