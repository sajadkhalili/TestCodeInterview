using SSF.Core.Paging;
using SSF.Core.Utilities;
using SSF.Infrastructure.Abstractions.DependencyInjections.Abstractions;
using SSF.Surveys.Domain.SubjectTypes.Contracts;
using SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypes;
using SSF.Surveys.Domain.SubjectTypes.Entities;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;
using SSF.Surveys.EF.Commands.Common;
using System.Threading;

namespace SSF.Surveys.EF.Commands.SubjectTypes;

public class SubjectTypeCommandRepository : ISubjectTypeCommandRepository, ITransientLifetime
{
    private readonly SurveyContext _context;

    public SubjectTypeCommandRepository ( SurveyContext context )
    {
        _context=context;
    }

    public async Task AddSubjectType ( SubjectType subjectType, CancellationToken cancellationToken=default )
    {
        await _context.AddAsync(subjectType,cancellationToken);
    }

    public async Task<SubjectType> GetSubjectTypeAsync ( SubjectTypeId subjectTypeId, CancellationToken cancellationToken = default )
    {
        return await _context.SubjectTypes.SingleAsync(st => st.Id==subjectTypeId&&st.IsDeleted==false, cancellationToken);
    }

    public async Task<PagedData<GetSubjectTypesResult>> GetSubjectTypesNoTrackingAsync ( GetSubjectTypesQuery query, CancellationToken cancellationToken )
    {
        var _query = _context.SubjectTypes.Where(x => x.IsDeleted==false).AsNoTracking();

        long count = 0;
        if (query.NeedTotalCount)
            count=await _query.CountAsync();

        var list = await _query.OrderByField(query.SortBy, query.SortAscending)
                                                        .Skip(query.SkipCount).Take(query.PageSize)
                                                             .Select(x => new GetSubjectTypesResult()
                                                             {
                                                                 SubjectTypeId=x.Id.Value,
                                                                 Title=x.Title.Value,
                                                                 IsActive=x.IsActive
                                                             }).ToListAsync();

        return new PagedData<GetSubjectTypesResult>
        {
            PageNumber=query.PageNumber,
            PageSize=query.PageSize,
            QueryResult=list,
            TotalCount=count
        };

    }

    public Task<int> SaveChangeAsync ( CancellationToken cancellationToken = default )
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void UpdateSubjectType ( SubjectType subjectType )
    {
        _context.SubjectTypes.Update(subjectType);
    }
}
