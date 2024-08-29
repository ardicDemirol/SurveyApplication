using MediatR;
using SurveyApplication.Dtos.SingleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.AddChoices;

public class AddSCQChoicesCommandHandlers(ISingleChoiceRepository singleChoiceRepository) : IRequestHandler<AddSCQChoicesCommandRequest>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;

    public async Task Handle(AddSCQChoicesCommandRequest request, CancellationToken cancellationToken)
    {
        var newSingleChoiceQuestionChoices = new SingleChoiceQuestionDto
        {
            First_Choice = request.First_Choice,
            Second_Choice = request.Second_Choice,
            Question_Id = request.Question_Id
        };

        await _singleChoiceRepository.AddChoice(newSingleChoiceQuestionChoices);
    }
}
