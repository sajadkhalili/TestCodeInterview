using SSF.Core.Paging;
using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypes;

namespace SSF.Surveys.Application.SubjectTypes.GetSubjectTypes
{
    public class GetSubjectTypesHandler : QueryHandler<GetSubjectTypesQuery, PagedData<GetSubjectTypesResult>>
    {
        private readonly ISubjectTypeCommandRepository _repository;

        public GetSubjectTypesHandler ( ISubjectTypeCommandRepository repository )
        {
            _repository=repository;
        }
        public override async Task<PagedData<GetSubjectTypesResult>> Handle ( GetSubjectTypesQuery query, CancellationToken cancellationToken )
        {
            return await _repository.GetSubjectTypesNoTrackingAsync(query, cancellationToken);
        }
    }
}
