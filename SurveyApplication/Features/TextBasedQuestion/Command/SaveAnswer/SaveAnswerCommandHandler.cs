using MediatR;
using SurveyApplication.Dtos.TextBasedDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.TextBasedQuestion.Command.SaveAnswer;

public class SaveAnswerCommandHandler(ITextBasedRepository textBasedRepository) : IRequestHandler<SaveAnswerCommandRequest>
{
    private readonly ITextBasedRepository _textBasedRepository = textBasedRepository;

    public async Task Handle(SaveAnswerCommandRequest request, CancellationToken cancellationToken)
    {
        var relation = new TextAnswersDto
        {
            Answer = request.Answer,
            Question_Id = request.Question_Id
        };

        await _textBasedRepository.SaveAnswer(relation);
    }
}
