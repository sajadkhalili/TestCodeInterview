namespace SSF.Surveys.Domain.SubjectTypes.Contracts.EditSubjectType
{
    public class EditSubjectTypeResult
    {
        public EditSubjectTypeResult ( int subjectTypeId, string title )
        {
            SubjectTypeId=subjectTypeId;
            Title=title;
        }

        public int SubjectTypeId { get; private set; }
        public string Title { get; private set; }
    }
}