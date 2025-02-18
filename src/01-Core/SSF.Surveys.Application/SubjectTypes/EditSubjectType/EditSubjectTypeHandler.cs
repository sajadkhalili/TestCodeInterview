using SSF.Core.Domain.Extentions;
using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.Contracts.EditSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Entities;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Application.SubjectTypes.EditSubjectType;

public class EditSubjectTypeHandler : CommandHandler<EditSubjectTypeCommand, EditSubjectTypeResult>
{
    private readonly ISubjectTypeCommandRepository _repository;

    public EditSubjectTypeHandler ( ISubjectTypeCommandRepository repository )
    {
        _repository=repository;
    }
    public override async Task<EditSubjectTypeResult> Handle ( EditSubjectTypeCommand command, CancellationToken cancellationToken )
    {
        SubjectType subjectType = await _repository.GetSubjectTypeAsync(new SubjectTypeId(command.SubjectTypeId), cancellationToken);
        subjectType.Edit(command.Title.ToPersianString());

        _repository.UpdateSubjectType(subjectType);
        await _repository.SaveChangeAsync(cancellationToken);

        return new EditSubjectTypeResult(subjectType.Id.ToInt(), subjectType.Title.ToString());
    }
}
