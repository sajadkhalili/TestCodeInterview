using SSF.Core.Utilities;
using SSF.Surveys.EndPoint.Api.Test.commons;
using Surveys.Provider.Api;
using System.Collections;
namespace SSF.Surveys.EndPoint.Api.Test.SubjectTypes;
public class EditSubjectTypeTest : BaseTest
{

    private readonly SubjectTypeClient _client;

    public EditSubjectTypeTest ( SurveyFixture surveyFixture ) : base(surveyFixture)
    {
        _client=surveyFixture.SubjectTypeClient;

    }
    [Fact]
    public async Task Edit_Success_When_Data_Is_Valid ()
    {

        var createResponse = await InsertData.CreateSubjectType();
   
        var editSubjectType = new EditSubjectTypeCommand()
        {
            SubjectTypeId=createResponse.Data.SubjectTypeId,
            Title=StringExtensions.GenerateRandomPersianString()
        };
        var EditResponse = await _client.EditAsync(editSubjectType);

        var newResponse = await _client.GetByIdAsync(createResponse.Data.SubjectTypeId);

        Assert.Equal(newResponse.Data.Title, editSubjectType.Title);
    }
    [Fact]
    public async Task Edit_Error_When_Title_Is_Duplicate ()
    {

        var firstCreateResponse = await InsertData.CreateSubjectType();

        var secondCreateResponse = await InsertData.CreateSubjectType();

        var editSubjectType = new EditSubjectTypeCommand()
        {
            SubjectTypeId=firstCreateResponse.Data.SubjectTypeId,
            Title=secondCreateResponse.Data.Title
        };

        var secondEditResponse = await _client.EditAsync(editSubjectType);

        Assert.NotNull(secondEditResponse);
        Assert.Equal(StatusCode.DuplicateError, secondEditResponse.Status);

    }

    [Fact]
    public async Task Edit_Error_When_Id_Does_Not_Exists ()
    {

        var editSubjectType = new EditSubjectTypeCommand()
        {
            SubjectTypeId=int.MaxValue,
            Title=StringExtensions.GenerateRandomPersianString()
        };

        // await Assert.ThrowsAnyAsync<Exception>(() => _client.EditAsync(editSubjectType));

        //   Todo: خطا هندل کنم
        var secondEditResponse = await _client.EditAsync(editSubjectType);

        Assert.NotNull(secondEditResponse);
        Assert.Equal(StatusCode.ServerException, secondEditResponse.Status);

    }
    public static IEnumerable<object[]> GetRandomPersianStrings ()
    {
        yield return new object[] { StringExtensions.GenerateRandomEnglishString() };
        yield return new object[] { null };
        yield return new object[] { "" };
        yield return new object[] { StringExtensions.GenerateRandomPersianString(2) };
        yield return new object[] { StringExtensions.GenerateRandomPersianString(31) };

    }

    [Theory]
    [MemberData(nameof(GetRandomPersianStrings))]
    public async Task Edit_Error_When_Data_Is_Invalid ( string title )
    {

        var createResponse = await InsertData.CreateSubjectType();

        var editResponse = await _client.EditAsync(new EditSubjectTypeCommand()
        { SubjectTypeId=createResponse.Data.SubjectTypeId, Title=title });

        Assert.NotNull(editResponse);
        Assert.Equal(StatusCode.ValidationError, editResponse.Status);
    }
    //[Theory]
    //[ClassData(typeof(ComplexInlineDataGenerator))]
    //public async Task Edit_Error_When_Data_Is_Invalid ( EditSubjectType editSubjectType )
    //{

    //    var createResponse = await InsertData.CreateSubjectType();

    //    editSubjectType.SubjectTypeId=createResponse.Data.SubjectTypeId;
    //    var EditResponse = await _client.EditAsync(editSubjectType);

    //    Assert.NotNull(EditResponse);
    //    Assert.Equal(StatusCode.ValidationError, EditResponse.Status);
    //}
    //public class ComplexInlineDataGenerator : IEnumerable<object[]>
    //{


    //    public IEnumerator<object[]> GetEnumerator()
    //    {
    //        yield return new object[] { new EditSubjectType { Text = "" } };
    //        yield return new object[] { new EditSubjectType { Text = null } };
    //        yield return new object[]
    //            { new EditSubjectType { Text = StringExtensions.GenerateRandomPersianString(2) } };
    //        yield return new object[]
    //            { new EditSubjectType { Text = StringExtensions.GenerateRandomPersianString(31) } };
    //        yield return new object[] { new EditSubjectType { Text = "notpersian" } };
    //    }

    //    IEnumerator IEnumerable.GetEnumerator () => GetEnumerator();
    //}
}
