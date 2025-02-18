
using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypeById;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Application.SubjectTypes.GetSubjectTypeById
{
    public class GetSubjectTypeByIdHandler : QueryHandler<GetSubjectTypeByIdQuery, GetSubjectTypeByIdResult>
    {
        private readonly ISubjectTypeCommandRepository _repository;

        public GetSubjectTypeByIdHandler ( ISubjectTypeCommandRepository repository )
        {
            _repository=repository;
        }
        public override async Task<GetSubjectTypeByIdResult> Handle ( GetSubjectTypeByIdQuery query, CancellationToken cancellationToken=default )
        {
            var res = await _repository.GetSubjectTypeAsync(new SubjectTypeId(query.SubjectTypeId), cancellationToken);
            return new GetSubjectTypeByIdResult(res.Id.Value, res.Title.Value, res.IsActive);

        }
    }
}
