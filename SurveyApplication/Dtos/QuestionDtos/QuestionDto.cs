namespace SurveyApplication.Dtos.QuestionDtos;

public sealed record QuestionDto
{
    public int Question_Id { get; }
    public string Question_Text { get; }
    public int Question_Order { get; }
    public char Question_Answer_Required { get; }
    public int Survey_Id { get; }
    public int Question_Type_Id { get; }

    private QuestionDto(string questionText, char questionAnswerRequired, int surveyId, int questionTypeId)
    {
        Question_Text = questionText;
        Question_Answer_Required = questionAnswerRequired;
        Survey_Id = surveyId;
        Question_Type_Id = questionTypeId;
    }

    public static QuestionDto Create(string questionText, char questionAnswerRequired, int surveyId, int questionTypeId)
    {
        return new QuestionDto(questionText, questionAnswerRequired, surveyId, questionTypeId);
    }
}



