using MediatR;
using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.AddChoices;

public class AddMCQChoicesCommandHandler(IMultipleChoiceRepository multipleChoiceRepository) : IRequestHandler<AddMCQChoiceCommandRequest>
{
    private readonly IMultipleChoiceRepository _multipleChoiceRepository = multipleChoiceRepository;
    public async Task Handle(AddMCQChoiceCommandRequest request, CancellationToken cancellationToken)
    {
        var newMultipleChoiceQuestionChoices = MultipleOtherChoicesDto.Create(request.Choice, request.MultipleChoiceQuestionId);

        await _multipleChoiceRepository.AddChoice(newMultipleChoiceQuestionChoices);
    }
}
