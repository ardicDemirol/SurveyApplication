using SurveyApplication.Dtos.MultipleChoiceDtos;

namespace SurveyApplication.Entities.MCQ;
public class MCQSaveChoices : Entity
{
    public string Choice { get; }
    public int Multiple_Choice_Question_Id { get; }

    private MCQSaveChoices(int id, string choice, int multipleChoiceQuestionId) : base(id)
    {
        Choice = choice;
        Multiple_Choice_Question_Id = multipleChoiceQuestionId;
    }

    public static MCQSaveChoices Create(int id, string choice, int multipleChoiceQuestionId)
    {
        return new MCQSaveChoices(id, choice, multipleChoiceQuestionId);
    }

    public static MCQSaveChoices FromDto(MCChoicesDto saveMCQChoicesDto)
    {
        return new MCQSaveChoices(
            saveMCQChoicesDto.Other_Choice_Id,
            saveMCQChoicesDto.Choice,
            saveMCQChoicesDto.Multiple_Choice_Question_Id);
    }
}
