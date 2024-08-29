namespace SurveyApplication.Features.MultipleChoiceQuestions.Queries.GetChoices;

public sealed record GetChoicesMCQQueryResponse
{
    public string Choices { get; set; }

    public GetChoicesMCQQueryResponse(string choice_Text)
    {
        Choices = choice_Text;
    }
}
