using MediatR;
using SurveyApplication.Dtos.TextBasedDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;

public class SaveAnswerCommandHandle(ITextBasedRepository textBasedRepository) : IRequestHandler<SaveAnswerCommandRequest>
{
    private readonly ITextBasedRepository _textBasedRepository = textBasedRepository;

    public async Task Handle(SaveAnswerCommandRequest request, CancellationToken cancellationToken)
    {
        var relation = TextAnswersDto.Create(request.Answer, request.Question_Id);
        await _textBasedRepository.SaveAnswer(relation);
    }
}
