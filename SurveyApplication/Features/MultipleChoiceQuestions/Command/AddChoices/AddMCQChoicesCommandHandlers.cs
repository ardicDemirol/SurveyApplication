using MediatR;
using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;

public class AddMCQChoicesCommandHandlers(IMultipleChoiceRepository multipleChoiceRepository) : IRequestHandler<AddMCQChoicesCommandRequest>
{
    private readonly IMultipleChoiceRepository _multipleChoiceRepository = multipleChoiceRepository;
    public async Task Handle(AddMCQChoicesCommandRequest request, CancellationToken cancellationToken)
    {
        var newMultipleChoiceQuestionChoices = new MultipleOtherChoicesDto
        {
            Choice = request.Choice,
            Multiple_Choice_Question_Id = request.Multiple_Choice_Question_Id
        };

        await _multipleChoiceRepository.AddChoice(newMultipleChoiceQuestionChoices);
    }
}
