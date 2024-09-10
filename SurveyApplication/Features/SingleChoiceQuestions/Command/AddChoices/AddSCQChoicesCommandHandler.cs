using MediatR;
using SurveyApplication.Dtos.SingleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;

public class AddSCQChoicesCommandHandler(ISingleChoiceRepository singleChoiceRepository) : IRequestHandler<AddSCQChoicesCommandRequest>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;

    public async Task Handle(AddSCQChoicesCommandRequest request, CancellationToken cancellationToken)
    {
        var newSingleChoiceQuestionChoices = SCQuestionDto.Create(request.First_Choice, request.Second_Choice, request.Question_Id);

        await _singleChoiceRepository.AddChoice(newSingleChoiceQuestionChoices);
    }
}
