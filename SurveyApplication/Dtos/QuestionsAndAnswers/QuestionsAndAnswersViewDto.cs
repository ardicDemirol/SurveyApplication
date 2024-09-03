namespace SurveyApplication.Dtos.QuestionsAndAnswers;

public class QuestionsAndAnswersViewDto
{
    public int Survey_Id { get; set; }
    public int Question_Id { get; set; }
    public string Question_Text { get; set; } = string.Empty;
    public string Choice { get; set; } = string.Empty;
    public string Answers { get; set; } = string.Empty;
}

