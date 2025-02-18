using SSF.Mediators.Contracts.Queries;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypeById;

public record GetSubjectTypeByIdQuery : IQuery<GetSubjectTypeByIdResult>
{

    public int SubjectTypeId { get; private set; }

    public GetSubjectTypeByIdQuery ( int subjectTypeId )
    {
        SubjectTypeId=subjectTypeId;
    }
}
