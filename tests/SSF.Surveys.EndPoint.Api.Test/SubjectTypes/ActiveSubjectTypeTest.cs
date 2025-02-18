using SSF.Surveys.EndPoint.Api.Test.commons;
using Surveys.Provider.Api;
namespace SSF.Surveys.EndPoint.Api.Test.SubjectTypes;
public class ActiveSubjectTypeTest : BaseTest
{

    private readonly SubjectTypeClient _client;

    public ActiveSubjectTypeTest ( SurveyFixture surveyFixture ) : base(surveyFixture)
    {
        _client=surveyFixture.SubjectTypeClient;

    }

    [Fact]
    public async Task Active_Success_When_Id_Is_Valid ()
    {
        var createResponse = await InsertData.CreateSubjectType();

        await _client.DeactivateAsync(createResponse.Data.SubjectTypeId);
        var response = await _client.GetByIdAsync(createResponse.Data.SubjectTypeId);
        Assert.False(response.Data.IsActive);

        await _client.ActivateAsync(createResponse.Data.SubjectTypeId);
        var newResponse = await _client.GetByIdAsync(createResponse.Data.SubjectTypeId);
        Assert.True(newResponse.Data.IsActive);
    }
}
