namespace SurveyApplication.Dtos.MultipleChoiceDtos;

public sealed record MultipleOtherChoicesDto
{
    public int Other_Choice_Id { get; }
    public string Choice { get; }
    public int Multiple_Choice_Question_Id { get; }

    private MultipleOtherChoicesDto(string choice, int multipleChoiceQuestionId)
    {
        Choice = choice;
        Multiple_Choice_Question_Id = multipleChoiceQuestionId;
    }

    public static MultipleOtherChoicesDto Create(string choice, int multipleChoiceQuestionId)
    {
        if (string.IsNullOrWhiteSpace(choice)) throw new ArgumentException("Choice cannot be empty.");

        if (multipleChoiceQuestionId <= 0) throw new ArgumentException("Multiple Choice Question ID must be greater than zero.");

        return new MultipleOtherChoicesDto(choice, multipleChoiceQuestionId);
    }
}

