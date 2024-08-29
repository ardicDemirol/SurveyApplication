using MediatR;
using SurveyApplication.Dtos.QuestionDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Questions.Command.CreateQuestion;

public class CreateQuestionCommandHandlers(IQuestionRepository questionRepository) : IRequestHandler<CreateQuestionCommandRequest>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;
    public async Task Handle(CreateQuestionCommandRequest request, CancellationToken cancellationToken)
    {
        var newQuestion = new QuestionDto
        {
            Question_Text = request.Question_Text,
            Question_Answer_Required = request.Question_Answer_Required,
            Survey_Id = request.Survey_Id,
            Question_Type_Id = request.Question_Type_Id
        };

        await _questionRepository.CreateQuestion(newQuestion);
    }
}