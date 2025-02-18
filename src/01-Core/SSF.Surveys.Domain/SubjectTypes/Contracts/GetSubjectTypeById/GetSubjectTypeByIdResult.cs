namespace SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypeById
{
    public record GetSubjectTypeByIdResult
    {
        public GetSubjectTypeByIdResult ( int subjectTypeId, string title, bool isActive )
        {
            SubjectTypeId=subjectTypeId;
            Title=title;
            IsActive=isActive;
        }

        public int SubjectTypeId { get; private set; }
        public string Title { get; private set; }
        public bool IsActive { get; private set; }
    }
}