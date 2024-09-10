using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Entities.TBQ;
using SurveyApplication.Interfaces;
using SurveyApplication.Mappers;
using SurveyApplication.Validations.ApplicationLayer.TextBasedValidations;

namespace SurveyApplication.Features.TextBasedQuestion.Command.SetRelation;

public class TBSetRelationCommandHandle(ITextBasedRepository textBasedRepository, TBSetRelationValidatorApp validator) : IRequestHandler<TBSetRelationCommandRequest, IActionResult>
{
    private readonly ITextBasedRepository _textBasedRepository = textBasedRepository;
    private readonly TBSetRelationValidatorApp _validator = validator;

    public async Task<IActionResult> Handle(TBSetRelationCommandRequest request, CancellationToken cancellationToken)
    {
        await _validator.QuestionExist(request);
        await _validator.RelationExist(request);
        await _validator.QuestionTypeIsCorrect(request);

        var relation = TBQSetRelationshipType.Create(0, request.Text_Type_Id, request.Question_Id);

        await _textBasedRepository.SetRelation(relation.ToDto());

        return new OkObjectResult("Relation Set Successfully");
    }
}
