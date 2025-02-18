using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Domain.SubjectTypes.Contracts.EditSubjectType;

public record EditSubjectTypeCommand : ICommand<EditSubjectTypeResult>
{
    public EditSubjectTypeCommand ( int subjectTypeId, string title )
    {
        SubjectTypeId=subjectTypeId;
        Title=title;
    }

    public int SubjectTypeId { get; private set; }
    public string Title { get; private set; }
}
