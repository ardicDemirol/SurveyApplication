namespace SurveyApplication.Views;

public class SingleChoiceQuestionChoicesView
{
    public int Question_Id { get; set; }
    public string Question_Text { get; set; } = string.Empty;
    public int Survey_Id { get; set; }
    public int Question_Type_Id { get; set; }
    public string First_Choice { get; set; } = string.Empty;
    public string Second_Choice { get; set; } = string.Empty;
}
