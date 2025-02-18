using SSF.Surveys.Domain.SubjectTypes.Contracts.RestoreSubjectType;

namespace SSF.Surveys.Application.SubjectTypes.RestoreSubjectType;

public class RestoreSubjectTypeHandler : CommandHandler<RestoreSubjectTypeCommand, RestoreSubjectTypeResult>
{
    public override Task<RestoreSubjectTypeResult> Handle ( RestoreSubjectTypeCommand request, CancellationToken cancellationToken )
    {
        throw new NotImplementedException();
    }
}
