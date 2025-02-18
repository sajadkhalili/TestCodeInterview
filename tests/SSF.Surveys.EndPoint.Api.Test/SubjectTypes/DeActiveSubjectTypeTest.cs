using SSF.Surveys.EndPoint.Api.Test.commons;
using Surveys.Provider.Api;
namespace SSF.Surveys.EndPoint.Api.Test.SubjectTypes;

public class DeActiveSubjectTypeTest : BaseTest
{
    private readonly SubjectTypeClient _client;

    public DeActiveSubjectTypeTest ( SurveyFixture surveyFixture ) : base(surveyFixture)
    {
        _client=surveyFixture.SubjectTypeClient;

    }
    [Fact]
    public async Task De_Active_Success_When_Id_Is_Valid ()
    {
        var createResponse = await InsertData.CreateSubjectType();
        await _client.DeactivateAsync(createResponse.Data.SubjectTypeId);
        var response = await _client.GetByIdAsync(createResponse.Data.SubjectTypeId);
        Assert.False(response.Data.IsActive);
    }
}
