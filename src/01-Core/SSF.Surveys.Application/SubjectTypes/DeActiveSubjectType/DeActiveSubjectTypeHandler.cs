using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.Contracts.DeActiveSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Entities;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Application.SubjectTypes.DeActiveSubjectType;

public class DeActiveSubjectTypeHandler : CommandHandler<DeActiveSubjectTypeCommand>
{
    private readonly ISubjectTypeCommandRepository _repository;

    public DeActiveSubjectTypeHandler ( ISubjectTypeCommandRepository repository )
    {
        _repository=repository;
    }
    public override async Task Handle ( DeActiveSubjectTypeCommand command, CancellationToken cancellationToken )
    {
        var subjectType = await _repository.GetSubjectTypeAsync(new SubjectTypeId(command.SubjectTypeId), cancellationToken);
        if(!subjectType.IsActive)
            return; 

        subjectType.DeActive();

        _repository.UpdateSubjectType(subjectType);
        await _repository.SaveChangeAsync(cancellationToken);
    }
}

