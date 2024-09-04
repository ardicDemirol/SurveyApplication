namespace SurveyApplication.Dtos.QuestionDtos;

public class QuestionChoicesViewDto
{
    public int Question_Id { get; set; }
    public string Question_Text { get; set; } = string.Empty;
    public int Survey_Id { get; set; }
    public int Question_Type_Id { get; set; }
    public string Choice { get; set; } = string.Empty;
}


//public class QuestionChoicesViewDto
//{
//    public int Question_Id { get; }
//    public string Question_Text { get; }
//    public int Survey_Id { get; }
//    public int Question_Type_Id { get; }
//    public string Choice { get; }

//    private QuestionChoicesViewDto(int questionId, string questionText, int surveyId, int questionTypeId, string choice)
//    {
//        Question_Id = questionId;
//        Question_Text = questionText;
//        Survey_Id = surveyId;
//        Question_Type_Id = questionTypeId;
//        Choice = choice;
//    }

//    public static QuestionChoicesViewDto Create(int questionId, string questionText, int surveyId, int questionTypeId, string choice)
//    {
//        if (string.IsNullOrWhiteSpace(questionText)) throw new ArgumentException("Question text cannot be empty.");

//        if (string.IsNullOrWhiteSpace(choice)) throw new ArgumentException("Choice cannot be empty.");

//        return new QuestionChoicesViewDto(questionId, questionText, surveyId, questionTypeId, choice);
//    }
//}

