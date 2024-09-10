using MediatR;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Surveys.Queries.GetAllSurveys;

public class GetAllSurveysQueryHandle(ISurveyRepository repository) : IRequestHandler<GetAllSurveysQueryRequest, IList<GetAllSurveysQueryResponse>>
{
    private readonly ISurveyRepository _repository = repository;

    public async Task<IList<GetAllSurveysQueryResponse>> Handle(GetAllSurveysQueryRequest request, CancellationToken cancellationToken)
    {
        var surveys = await _repository.GetAllSurveys<GetAllSurveysQueryResponse>();

        List<GetAllSurveysQueryResponse> response = [];

        foreach (var survey in surveys)
        {
            response.Add(new GetAllSurveysQueryResponse(
                survey.Survey_Id,
                survey.Survey_Title
                ));
        }

        return response;
    }
}

