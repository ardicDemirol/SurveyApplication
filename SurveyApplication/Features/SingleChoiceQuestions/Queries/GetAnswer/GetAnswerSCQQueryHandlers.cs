using MediatR;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.SingleChoiceQuestions.Queries.GetAnswer;

public class GetAnswerSCQQueryHandlers(ISingleChoiceRepository singleChoiceRepository) : IRequestHandler<GetAnswerSCQQueryRequest, GetAnswerSCQQueryResponse>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;
    public async Task<GetAnswerSCQQueryResponse> Handle(GetAnswerSCQQueryRequest request, CancellationToken cancellationToken)
    {
        var answer = await _singleChoiceRepository.GetAnswer<GetAnswerSCQQueryResponse>(request.QuestionID);

        return answer;
    }
}
