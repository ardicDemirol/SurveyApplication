using MediatR;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Surveys.Queries.GetAnswerSingleChoiceQuestions;

public class GetAnswerSingleChoiceQuestionsQueryHandlers(ISingleChoiceRepository singleChoiceRepository) : IRequestHandler<GetAnswerSingleChoiceQuestionsQueryRequest, GetAnswerSingleChoiceQuestionsQueryResponse>
{
    private readonly ISingleChoiceRepository _singleChoiceRepository = singleChoiceRepository;
    public async Task<GetAnswerSingleChoiceQuestionsQueryResponse> Handle(GetAnswerSingleChoiceQuestionsQueryRequest request, CancellationToken cancellationToken)
    {
        var answer = await _singleChoiceRepository.GetAnswer<GetAnswerSingleChoiceQuestionsQueryResponse>(request.QuestionID, request.SurveyID);

        return answer;
    }
}
