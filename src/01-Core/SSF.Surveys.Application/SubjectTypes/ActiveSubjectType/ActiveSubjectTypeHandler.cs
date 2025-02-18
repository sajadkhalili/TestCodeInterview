using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.Contracts.ActiveSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Entities;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Application.SubjectTypes.ActiveSubjectType;

public class ActiveSubjectTypeHandler : CommandHandler<ActiveSubjectTypeCommand>
{
    private readonly ISubjectTypeCommandRepository _repository;

    public ActiveSubjectTypeHandler ( ISubjectTypeCommandRepository repository )
    {
        _repository=repository;
    }
    public override async Task Handle ( ActiveSubjectTypeCommand command, CancellationToken cancellationToken = default )
    {
        var subjectType = await _repository.GetSubjectTypeAsync(command.SubjectTypeId, cancellationToken);
        if(subjectType.IsActive)
            return;

        subjectType.Active();

        _repository.UpdateSubjectType(subjectType);
        await _repository.SaveChangeAsync(cancellationToken);
    }
}
