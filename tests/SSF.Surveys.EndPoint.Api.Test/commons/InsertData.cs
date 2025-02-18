using SSF.Core.Utilities;
using Surveys.Provider.Api;
namespace SSF.Surveys.EndPoint.Api.Test.commons;

public static class InsertData
{
    private static SubjectTypeClient _subjectTypeClient;
     private static SubjectClient _subjectClient;
    public static void CreateInstance ( SurveyFixture surveyFixture )
    {
        _subjectTypeClient=surveyFixture.SubjectTypeClient;
        _subjectClient = surveyFixture.SubjectClient;

    }


    public static async Task<List<ApiResultOfCreateSubjectTypeResult>> CreateSubjectType ( byte subjectTypeCount,  string? title = null )
    {
        var results = new List<ApiResultOfCreateSubjectTypeResult>();
        for (var i = 1; i <= subjectTypeCount; i++)
        {
            results.Add( await CreateSubjectType ( title));
        }

        return results;
    }
   public static async Task<ApiResultOfCreateSubjectTypeResult> CreateSubjectType (  string? title = null )
    {

        var createSubjectType = new CreateSubjectTypeCommand()
        {
            Title=title??StringExtensions.GenerateRandomPersianString()
        };


        var createResponse = await _subjectTypeClient.CreateAsync(createSubjectType);
        var getByIdResponse = await _subjectTypeClient.GetByIdAsync(createResponse.Data.SubjectTypeId);

        Assert.NotNull(createResponse);
        Assert.Equal(StatusCode.Success, createResponse.Status);
        Assert.Equal(getByIdResponse.Data.Title, createSubjectType.Title);
        return createResponse;
    }

    public static async Task<ApiResultOfCreateSubjectResult> CreateSubject ( byte subjectTypeCount=1, string? title = null )
    {
        var subjectType = await CreateSubjectType(subjectTypeCount: subjectTypeCount);
        var newSubject = new CreateSubjectCommand
        {
            Title=title?? StringExtensions.GenerateRandomPersianString(),
            Scope=SubjectScope.City,
            Description=StringExtensions.GenerateRandomMixedString(),
            SubjectTypeIds=subjectType.Select(st => st.Data.SubjectTypeId).ToList()
        };

        var createResponse = await _subjectClient.CreateAsync(newSubject);
        var getByIdResponse = await _subjectClient.GetByIdAsync(createResponse.Data.SubjectId);

        Assert.NotNull(createResponse);
        Assert.Equal(StatusCode.Success, createResponse.Status);
        Assert.Equal(getByIdResponse.Data.Title, newSubject.Title);
        Assert.Equal(getByIdResponse.Data.Description, newSubject.Description);
        Assert.Equal(getByIdResponse.Data.Scope, newSubject.Scope);
        Assert.Equal(getByIdResponse.Data.SubjectTypes.Select(st => st.SubjectTypeId), newSubject.SubjectTypeIds);
        return createResponse;
    }
   
}

