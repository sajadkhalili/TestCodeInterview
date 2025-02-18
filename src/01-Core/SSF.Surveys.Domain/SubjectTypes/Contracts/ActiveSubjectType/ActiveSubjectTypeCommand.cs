using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Domain.SubjectTypes.Contracts.ActiveSubjectType;

public record ActiveSubjectTypeCommand : ICommand
{
    public ActiveSubjectTypeCommand ( SubjectTypeId subjectTypeId )
    {
        SubjectTypeId=subjectTypeId;
    }
    public SubjectTypeId SubjectTypeId { get; }
}

