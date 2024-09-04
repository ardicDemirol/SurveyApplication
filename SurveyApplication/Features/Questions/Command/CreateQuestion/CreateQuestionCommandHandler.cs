using MediatR;
using SurveyApplication.Dtos.QuestionDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.Questions.Command.CreateQuestion;

public class CreateQuestionCommandHandler(IQuestionRepository questionRepository) : IRequestHandler<CreateQuestionCommandRequest>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;
    public async Task Handle(CreateQuestionCommandRequest request, CancellationToken cancellationToken)
    {
        var newQuestion = QuestionDto.Create(request.Question_Text, request.Question_Answer_Required, request.Survey_Id, request.Question_Type_Id);

        await _questionRepository.CreateQuestion(newQuestion);

        //return Results.Ok("Question created successfuly");
    }
}
