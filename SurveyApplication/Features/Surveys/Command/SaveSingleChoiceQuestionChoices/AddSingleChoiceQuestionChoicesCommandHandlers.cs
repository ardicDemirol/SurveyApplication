using MediatR;
using SurveyApplication.Dtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Surveys.Command.SaveSingleChoiceQuestionChoices;

public class AddSingleChoiceQuestionChoicesCommandHandlers(ISingleChoiceRepository singleChoiceRepository) : IRequestHandler<AddSingleChoiceQuestionChoicesCommandRequest>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;

    public async Task Handle(AddSingleChoiceQuestionChoicesCommandRequest request, CancellationToken cancellationToken)
    {
        var newSingleChoiceQuestionChoices = new SingleChoiceQuestionDto
        {
            First_Choice = request.First_Choice,
            Second_Choice = request.Second_Choice,
            Question_Id = request.Question_Id
        };

        await _singleChoiceRepository.AddChoices(newSingleChoiceQuestionChoices);
    }
}
