using SSF.Core.Domain.ValueObjects;
using SSF.Infrastructure.Abstractions.DependencyInjections.Abstractions;
using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;
using SSF.Surveys.EF.Commands.Common;

namespace SSF.Surveys.EF.Commands.SubjectTypes;

public class SubjectTypeRuleRepository : ISubjectTypeRuleRepository, IScopeLifetime
{
    private readonly SurveyContext _context;

    public SubjectTypeRuleRepository ( SurveyContext context )
    {
        _context=context;
    }
    public async Task<bool> IsExist ( PersianString title, CancellationToken cancellationToken = default )
    {

        return await _context.SubjectTypes.AnyAsync(s => s.Title==title, cancellationToken);

    }

    public async Task<bool> IsExist ( PersianString title, SubjectTypeId expectSubjectTypeId, CancellationToken cancellationToken = default )
    {
        return await _context.SubjectTypes
            .AnyAsync(s => s.Title==title&&s.Id!=expectSubjectTypeId
            , cancellationToken);
    }
}