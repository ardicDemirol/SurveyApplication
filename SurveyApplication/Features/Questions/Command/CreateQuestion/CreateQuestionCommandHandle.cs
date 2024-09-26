using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Entities.Question;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.QuestionValidations;

namespace SurveyApplication.Features.Questions.Command.CreateQuestion;

public class CreateQuestionCommandHandle(
    IQuestionRepository questionRepository,
    CreateQuestionValidatorApp createQuestionCommandValidator) : IRequestHandler<CreateQuestionCommandRequest, IActionResult>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;
    private readonly CreateQuestionValidatorApp _createQuestionCommandValidator = createQuestionCommandValidator;


    public async Task<IActionResult> Handle(CreateQuestionCommandRequest request, CancellationToken cancellationToken)
    {

        await _createQuestionCommandValidator.SurveyExist(request);
        await _createQuestionCommandValidator.QuestionExist(request);

        var newQuestion = Question.Create(0, request.Question_Text, request.Question_Answer_Required, request.Survey_Id, request.Question_Type_Id);

        await _questionRepository.CreateQuestion(newQuestion.ToDto());

        return new OkObjectResult("Question Created Successfully");
    }
}
