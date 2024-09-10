using MediatR;
using SurveyApplication.Dtos.SingleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.SingleChoiceQuestions.Command.SavesAnswer;

public class SaveSCACommandHandler(ISingleChoiceRepository singleChoiceRepository) : IRequestHandler<SaveSCACommandRequest>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;

    public async Task Handle(SaveSCACommandRequest request, CancellationToken cancellationToken)
    {
        var newSingleChoiceAnswer = SCAnswerDto.Create(request.Answer, request.Question_Id, request.Survey_Id);
        await _singleChoiceRepository.SaveAnswer(newSingleChoiceAnswer);
    }
}
