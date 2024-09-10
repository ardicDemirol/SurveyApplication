namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public sealed record MCChoicesDto
{
    public int Other_Choice_Id { get; }
    public string Choice { get; }
    public int Multiple_Choice_Question_Id { get; }

    private MCChoicesDto(string choice, int multipleChoiceQuestionId)
    {
        Choice = choice;
        Multiple_Choice_Question_Id = multipleChoiceQuestionId;
    }

    public static MCChoicesDto Create(string choice, int multipleChoiceQuestionId)
    {
        return new MCChoicesDto(choice, multipleChoiceQuestionId);
    }
}

