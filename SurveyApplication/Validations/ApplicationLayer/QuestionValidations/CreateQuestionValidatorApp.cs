﻿using SurveyApplication.Features.Questions.Command.CreateQuestion;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Validations.ApplicationLayer.QuestionValidations;

public sealed class CreateQuestionValidatorApp(IQuestionRepository repository)
{
    private readonly IQuestionRepository _repository = repository;

    public async Task QuestionExist(CreateQuestionCommandRequest request)
    {
        var questionExists = await _repository.QuestionExist(request.Survey_Id, request.Question_Text);

        if (questionExists)
        {
            throw new Exception("Question with this text already exists");
        }

    }

}
