using MediatR;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.QuestionsAndAnswers.Queries.GetQuestionsAndAnswers;
public class GetQuestionsAndAnswersQueryHandle(IQuestionsAndAnswersRepository questionsAndAnswersRepository) : IRequestHandler<GetQuestionsAndAnswersQueryRequest, IList<GetQuestionsAndAnswersQueryResponse>>
{
    private readonly IQuestionsAndAnswersRepository _questionsAndAnswersRepository = questionsAndAnswersRepository;
    public async Task<IList<GetQuestionsAndAnswersQueryResponse>> Handle(GetQuestionsAndAnswersQueryRequest request, CancellationToken cancellationToken)
    {
        var questionsAndAnswers = await _questionsAndAnswersRepository.GetAllQuestionsAndAnswers<GetQuestionsAndAnswersQueryResponse>(request.Survey_Id);

        List<GetQuestionsAndAnswersQueryResponse> response = [];

        foreach (var questionAndAnswer in questionsAndAnswers)
            response.Add(new GetQuestionsAndAnswersQueryResponse(
                questionAndAnswer.Question_Id,
                questionAndAnswer.Question_Text,
                questionAndAnswer.Answers
            ));

        return response;
    }
}
