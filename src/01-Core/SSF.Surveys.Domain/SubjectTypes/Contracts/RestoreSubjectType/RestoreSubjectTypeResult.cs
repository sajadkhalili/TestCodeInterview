namespace SSF.Surveys.Domain.SubjectTypes.Contracts.RestoreSubjectType
{
    public record RestoreSubjectTypeResult
    {
        public RestoreSubjectTypeResult ( int subjectTypeId, string title )
        {
            SubjectTypeId=subjectTypeId;
            Title=title;
        }

        public int SubjectTypeId { get; private set; }
        public string Title { get; private set; }
    }
}