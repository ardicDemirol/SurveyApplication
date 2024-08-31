﻿using MediatR;
using SurveyApplication.Dtos.TextBasedDtos;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;

public class SetRelationCommandHandler(ITextBasedRepository textBasedRepository) : IRequestHandler<SetRelationCommandRequest>
{
    private readonly ITextBasedRepository _textBasedRepository = textBasedRepository;
    public async Task Handle(SetRelationCommandRequest request, CancellationToken cancellationToken)
    {
        var relation = new TextBasedQuestionTypeRelationshipDto
        {
            Text_Type_Id = request.Text_Type_Id,
            Question_Id = request.Question_Id
        };

        await _textBasedRepository.SetRelation(relation);
    }
}
