using MediatR;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.MultipleChoiceQuestions.Queries.GetChoices;
public class GetChoiecesMCQQueryHandlers(IMultipleChoiceRepository multipleChoiceRepository) : IRequestHandler<GetChoicesMCQQueryRequest, IList<GetChoicesMCQQueryResponse>>
{
    private readonly IMultipleChoiceRepository _multipleChoiceRepository = multipleChoiceRepository;

    public async Task<IList<GetChoicesMCQQueryResponse>> Handle(GetChoicesMCQQueryRequest request, CancellationToken cancellationToken)
    {
        var choices = await _multipleChoiceRepository.GetChoices<GetChoicesMCQQueryResponse>(request.QuestionId);

        List<GetChoicesMCQQueryResponse> response = [];


        foreach (var choice in choices)
        {
            response.Add(new GetChoicesMCQQueryResponse(
                choice.Choices
            ));
        }

        return response;
    }
}
