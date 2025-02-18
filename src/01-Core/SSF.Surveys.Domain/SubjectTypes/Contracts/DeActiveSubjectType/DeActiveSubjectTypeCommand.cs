using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Domain.SubjectTypes.Contracts.DeActiveSubjectType;

public record DeActiveSubjectTypeCommand : ICommand
{
    public DeActiveSubjectTypeCommand ( int subjectTypeId )
    {
        SubjectTypeId=subjectTypeId;
    }
    public int SubjectTypeId { get; }
}
