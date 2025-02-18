using SSF.Surveys.EF.Commands.Common;
using SSF.Surveys.EndPoint.Api.Test.commons;
using Surveys.Provider.Api;
namespace SSF.Surveys.EndPoint.Api.Test.SubjectTypes;

public class DeleteSubjectTypeTest : BaseTest
{
    private readonly SubjectTypeClient _client;
    private readonly SurveyContext _context;

    public DeleteSubjectTypeTest ( SurveyFixture surveyFixture ) : base(surveyFixture)
    {
        _client=surveyFixture.SubjectTypeClient;
        _context=surveyFixture.DbContext;

    }
    [Fact]
    public async Task Delete_Success_When_Id_Is_Valid ()
    {
        var createResponse = await InsertData.CreateSubjectType();
        await _client.DeleteAsync(createResponse.Data.SubjectTypeId);

        var response = await _client.GetByIdAsync(createResponse.Data.SubjectTypeId);
        Assert.Equal(StatusCode.ServerException, response.Status);

    }

    [Fact]
    public async Task Delete_Error_When_Id_Does_Not_Exists ()
    {
        var response = await _client.GetByIdAsync(int.MaxValue);
        Assert.Equal(StatusCode.ServerException, response.Status);
    }
}
