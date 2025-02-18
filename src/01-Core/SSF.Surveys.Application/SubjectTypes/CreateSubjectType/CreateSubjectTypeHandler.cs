using SSF.Core.Domain.Extentions;
using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.Contracts.CreateSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Entities;

namespace SSF.Surveys.Application.SubjectTypes.CreateSubjectType;

public class CreateSubjectTypeHandler : CommandHandler<CreateSubjectTypeCommand, CreateSubjectTypeResult>
{
    private readonly ISubjectTypeCommandRepository _repository;

    public CreateSubjectTypeHandler ( ISubjectTypeCommandRepository repository )
    {
        _repository=repository;

    }
    public override async Task<CreateSubjectTypeResult> Handle ( CreateSubjectTypeCommand command, CancellationToken cancellationToken  )
    {

        var subjectType = new SubjectType(command.Title.ToPersianString());

        await _repository.AddSubjectType(subjectType,cancellationToken);
        await _repository.SaveChangeAsync(cancellationToken);
        return new CreateSubjectTypeResult(subjectType.Id.Value, subjectType.Title.Value);
    }
}
