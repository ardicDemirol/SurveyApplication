using MediatR;
using SurveyApplication.Dtos.SingleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;

public class SaveSCACommandHandlers(ISingleChoiceRepository singleChoiceRepository) : IRequestHandler<SaveSCACommandRequest>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;

    public async Task Handle(SaveSCACommandRequest request, CancellationToken cancellationToken)
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
