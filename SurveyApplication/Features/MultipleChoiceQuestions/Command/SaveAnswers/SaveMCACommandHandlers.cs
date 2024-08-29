using MediatR;
using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SaveAnswers;

public class SaveMCACommandHandlers(IMultipleChoiceRepository multipleChoiceRepository) : IRequestHandler<SaveMCACommandRequest>
{
    private readonly IMultipleChoiceRepository _multipleChoiceRepository = multipleChoiceRepository;
    public async Task Handle(SaveMCACommandRequest request, CancellationToken cancellationToken)
    {
        var answer = new MultipleChoiceAnswersDto
        {
            Answer = request.Answer,
            Question_Id = request.QuestionID
        };

        await _multipleChoiceRepository.SaveAnswer(answer);
    }
}
