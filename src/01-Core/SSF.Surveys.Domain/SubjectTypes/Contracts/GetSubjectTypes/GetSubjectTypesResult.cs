namespace SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypes
{
    public record GetSubjectTypesResult
    {

        public int SubjectTypeId { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}