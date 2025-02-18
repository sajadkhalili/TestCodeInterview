using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.Contracts.DeleteSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Entities;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Application.SubjectTypes.DeleteSubjectType;

public class DeleteSubjectTypeHandler : CommandHandler<DeleteSubjectTypeCommand>
{
    private readonly ISubjectTypeCommandRepository _repository;

    public DeleteSubjectTypeHandler ( ISubjectTypeCommandRepository repository )
    {
        _repository=repository;
    }
    public override async Task Handle ( DeleteSubjectTypeCommand command, CancellationToken cancellationToken )
    {
        var subjectType = await _repository.GetSubjectTypeAsync(new SubjectTypeId(command.SubjectTypeId), cancellationToken);
       
        if(subjectType.IsDeleted)
            return;

        subjectType.Delete();

        _repository.UpdateSubjectType(subjectType);
        await _repository.SaveChangeAsync(cancellationToken);
    }
}
