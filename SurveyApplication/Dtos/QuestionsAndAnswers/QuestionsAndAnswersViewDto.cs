namespace SurveyApplication.Dtos.QuestionsAndAnswers;

public sealed record QuestionsAndAnswersViewDto
{
    public int Survey_Id { get; set; }
    public int Question_Id { get; set; }
    public string Question_Text { get; set; } = string.Empty;
    public string Answers { get; set; } = string.Empty;
}

//public sealed record QuestionsAndAnswersViewDto
//{
//    public int Survey_Id { get; }
//    public int Question_Id { get; }
//    public string Question_Text { get; } = string.Empty;
//    public string Answers { get; } = string.Empty;

//    private QuestionsAndAnswersViewDto(int surveyId) => Survey_Id = surveyId;

//    public static QuestionsAndAnswersViewDto Create(int surveyId)
//    {
//        return new QuestionsAndAnswersViewDto(surveyId);
//    }
//}


