using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.Domain.SubjectTypes.Contracts;

public interface ISubjectTypeRuleRepository
{
    Task<bool> IsExist ( PersianString title, CancellationToken cancellationToken = default );
    Task<bool> IsExist ( PersianString title, SubjectTypeId expectId, CancellationToken cancellationToken = default );
}
