using Microsoft.AspNetCore.Mvc.Filters;
using SSF.Surveys.Domain.SubjectTypes.Contracts.ActiveSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Contracts.CreateSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Contracts.DeActiveSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Contracts.DeleteSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Contracts.EditSubjectType;
using SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypeById;
using SSF.Surveys.Domain.SubjectTypes.Contracts.GetSubjectTypes;
using System.Diagnostics;
using SSF.Surveys.Domain.SubjectTypes.ValueObjects;

namespace SSF.Surveys.EndPoint.Api.Controllers;

public class SubjectTypeController : SurveysApiController
{

  
    [HttpPost]
    public async Task<ApiResult<CreateSubjectTypeResult>> Create ( CreateSubjectTypeCommand request, CancellationToken cancellationToken )
    {
      
        return await _SSFMediator.SendCommandAsync<CreateSubjectTypeCommand, CreateSubjectTypeResult>(request, cancellationToken);
     
    }

    [HttpPut]
    public async Task<ApiResult<EditSubjectTypeResult>> Edit ( EditSubjectTypeCommand request, CancellationToken cancellationToken )
    {
      
        return await _SSFMediator.SendCommandAsync<EditSubjectTypeCommand, EditSubjectTypeResult>(request, cancellationToken);
    }

    [HttpPatch("activate/{id:int}")]
    public async Task<ApiResult> Activate ( int id, CancellationToken cancellationToken )
    {
        return await _SSFMediator.SendCommandAsync(new ActiveSubjectTypeCommand(SubjectTypeId.FromInt(id)), cancellationToken);
    }

    [HttpPatch("deactivate/{id:int}")]
    public async Task<ApiResult> Deactivate ( int id, CancellationToken cancellationToken )
    {
        var result = await _SSFMediator.SendCommandAsync(new DeActiveSubjectTypeCommand(id), cancellationToken);
        return result;
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<ApiResult> Delete ( int id, CancellationToken cancellationToken )
    {
        var result = await _SSFMediator.SendCommandAsync(new DeleteSubjectTypeCommand(id), cancellationToken);
        return result;
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResult<GetSubjectTypeByIdResult>> GetById ( int id, CancellationToken cancellationToken )
    {
        var result = await _SSFMediator.ExecuteQueryAsync<GetSubjectTypeByIdQuery, GetSubjectTypeByIdResult>(new GetSubjectTypeByIdQuery(id), cancellationToken);
        return result;
    }

    [HttpGet("all/{pageNumber}/{pageSize}")]
    public async Task<Result<PagedData<GetSubjectTypesResult>>> Get ( int pageNumber, int pageSize, CancellationToken cancellationToken )
    {

        var result = await _SSFMediator.ExecuteQueryAsync<GetSubjectTypesQuery, PagedData<GetSubjectTypesResult>>(new GetSubjectTypesQuery(), cancellationToken);
        return result;

    }
}
