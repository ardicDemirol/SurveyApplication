using MediatR;
using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;

public class SaveMCACommandHandler(IMultipleChoiceRepository multipleChoiceRepository) : IRequestHandler<SaveMCACommandRequest>
{
    private readonly IMultipleChoiceRepository _multipleChoiceRepository = multipleChoiceRepository;
    public async Task Handle(SaveMCACommandRequest request, CancellationToken cancellationToken)
    {
        var answer = MultipleChoiceAnswersDto.Create(request.Answer, request.QuestionId);

        await _multipleChoiceRepository.SaveAnswer(answer);
    }
}
