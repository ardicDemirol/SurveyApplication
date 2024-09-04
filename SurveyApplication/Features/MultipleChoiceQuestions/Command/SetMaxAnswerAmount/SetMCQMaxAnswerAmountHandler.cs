using MediatR;
using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

public class SetMCQMaxAnswerAmountHandler(IMultipleChoiceRepository multipleChoiceRepository) : IRequestHandler<SetMCQMaxAnswerAmountRequest>
{
    private readonly IMultipleChoiceRepository _multipleChoiceRepository = multipleChoiceRepository;

    public async Task Handle(SetMCQMaxAnswerAmountRequest request, CancellationToken cancellationToken)
    {
        var question = MultipleChoiceDto.Create(request.MaxChoiceAmount, request.QuestionID);

        int result = await _multipleChoiceRepository.SetMaxAnswerAmount(question);
        throw new Exception($"Choice Id is {result}");

    }
}





