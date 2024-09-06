using MediatR;
using SurveyApplication.Dtos.MultipleChoiceDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Command.SetMaxAnswerAmount;

public class SetMCQMaxAnswerAmountHandler(IMultipleChoiceRepository multipleChoiceRepository) : IRequestHandler<SetMCQMaxAnswerAmountRequest, int>
{
    private readonly IMultipleChoiceRepository _multipleChoiceRepository = multipleChoiceRepository;

    public async Task<int> Handle(SetMCQMaxAnswerAmountRequest request, CancellationToken cancellationToken)
    {
        var question = MultipleChoiceDto.Create(request.MaxChoiceAmount, request.QuestionID);

        return await _multipleChoiceRepository.SetMaxAnswerAmount(question);
    }
}





