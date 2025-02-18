using SSF.Core.Paging;
using SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypes;
using SSF.Surveys.Domain.SubjectTypes.Entities;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;
using System.Threading;

namespace SSF.Surveys.Domain.SubjectTypes.Contracts;

public interface ISubjectTypeCommandRepository
{
    Task AddSubjectType ( SubjectType subjectType, CancellationToken cancellationToken=default );
    Task<SubjectType> GetSubjectTypeAsync ( SubjectTypeId subjectTypeId, CancellationToken cancellationToken = default );

    Task<PagedData<GetSubjectTypesResult>> GetSubjectTypesNoTrackingAsync ( GetSubjectTypesQuery query, CancellationToken cancellationToken );
    Task<int> SaveChangeAsync ( CancellationToken cancellationToken = default );
    void UpdateSubjectType ( SubjectType subjectType );
}
