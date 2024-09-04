using MediatR;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Questions.Queries.GetAllSurveyQuestions;

public class GetAllSurveyQuestionsQueryHandler(IQuestionRepository questionRepository) : IRequestHandler<GetAllSurveyQuestionsQueryRequest, IList<GetAllSurveyQuestionsQueryResponse>>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;
    public async Task<IList<GetAllSurveyQuestionsQueryResponse>> Handle(GetAllSurveyQuestionsQueryRequest request, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetAllSurveyQuestions<GetAllSurveyQuestionsQueryResponse>(request.Survey_Id);

        List<GetAllSurveyQuestionsQueryResponse> response = [];

        foreach (var question in questions)
            response.Add(new GetAllSurveyQuestionsQueryResponse(
                question.Question_Id,
                question.Question_Text,
                question.Choice
            ));

        return response;
    }
}
