namespace SurveyApplication.Dtos.QuestionDtos;

public class QuestionChoicesViewDto
{
    public int Question_Id { get; set; }
    public string Question_Text { get; set; } = string.Empty;
    public int Survey_Id { get; set; }
    public int Question_Type_Id { get; set; }
    public string Choice { get; set; } = string.Empty;
}
