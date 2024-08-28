using MediatR;
using SurveyApplication.Dtos;
using SurveyApplication.Features.Surveys.Command.CreateSingleChoiceAnswer;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Surveys.Command.SaveSingleChoiceAnswer;

public class SaveSingleChoiceAnswerCommandHandlers(ISingleChoiceRepository singleChoiceRepository) : IRequestHandler<SaveSingleChoiceAnswerCommandRequest>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;

    public async Task Handle(SaveSingleChoiceAnswerCommandRequest request, CancellationToken cancellationToken)
    {
        var newSingleChoiceAnswer = new SingleChoiceAnswerDto
        {
            Answer = request.Answer,
            Question_Id = request.Question_Id,
            Survey_Id = request.Survey_Id
        };

        await _singleChoiceRepository.SaveAnswer(newSingleChoiceAnswer);
    }
}
