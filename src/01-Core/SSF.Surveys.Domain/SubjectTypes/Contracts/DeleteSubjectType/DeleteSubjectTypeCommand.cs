using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Domain.SubjectTypes.Contracts.DeleteSubjectType;

public record DeleteSubjectTypeCommand : ICommand
{
    public DeleteSubjectTypeCommand ( int subjectTypeId )
    {
        SubjectTypeId=subjectTypeId;
    }
    public int SubjectTypeId { get; }
}

