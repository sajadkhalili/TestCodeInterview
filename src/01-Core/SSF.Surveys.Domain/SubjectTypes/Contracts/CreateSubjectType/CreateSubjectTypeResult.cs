namespace SSF.Surveys.Domain.SubjectTypes.Contracts.CreateSubjectType;

public record CreateSubjectTypeResult
{
    public int SubjectTypeId { get; private set; }
    public string Title { get; private set; }
    public CreateSubjectTypeResult ( int subjectTypeId, string title )
    {
        SubjectTypeId=subjectTypeId;
        Title=title;
    }
}