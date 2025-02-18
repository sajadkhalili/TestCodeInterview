using SSF.Core.Utilities;
using SSF.Surveys.EndPoint.Api.Test.commons;
using Surveys.Provider.Api;
using System.Collections;
namespace SSF.Surveys.EndPoint.Api.Test.SubjectTypes;

public class CreateSubjectTypeTest : BaseTest
{

    private SubjectTypeClient _client;

    public CreateSubjectTypeTest ( SurveyFixture surveyFixture ) : base(surveyFixture)
    {
        _client=surveyFixture.SubjectTypeClient;
    }

    [Fact]
    public async Task Create_Success_When_Data_Is_Valid ()
    {

        var newSubjectType = new CreateSubjectTypeCommand()
        {
            Title=StringExtensions.GenerateRandomPersianString()
        };

        var createResponse = await _client.CreateAsync(newSubjectType);
        var getByIdResponse = await _client.GetByIdAsync(createResponse.Data.SubjectTypeId);

        Assert.NotNull(createResponse);
        Assert.Equal(StatusCode.Success, createResponse.Status);
        Assert.Equal(getByIdResponse.Data.Title, newSubjectType.Title);
    }

    [Fact]
    public async Task Create_Error_When_Title_Is_Duplicate ()
    {
        var title = StringExtensions.GenerateRandomPersianString();
        await InsertData.CreateSubjectType(title: title);
        var secondCreateResponse = await _client.CreateAsync(new CreateSubjectTypeCommand() { Title=title });

        Assert.NotNull(secondCreateResponse);
        Assert.Equal(StatusCode.DuplicateError, secondCreateResponse.Status);

    }

    //[Fact]
    //public async Task Create_Error_When_Required_Field_Is_Missing()
    //{

    //    var newSubjectType = new CreateSubjectType()
    //    {
    //        Text=null
    //    };

    //    CommandResultOfCreateSubjectTypeResult? createResponse = await _client.CreateAsync(newSubjectType);

    //    Assert.NotNull(createResponse);
    //    Assert.Equal(Status.ValidationError, createResponse.Status);
    //}

    [Theory]
    [ClassData(typeof(ComplexCreateSubjectTypeDataGenerator))]
    public async Task Create_Error_When_Data_Is_Invalid ( CreateSubjectTypeCommand newSubjectType )
    {
        var createResponse = await _client.CreateAsync(newSubjectType);

        Assert.NotNull(createResponse);
        Assert.Equal(StatusCode.ValidationError, createResponse.Status);
    }
    public class ComplexCreateSubjectTypeDataGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator ()
        {
            yield return new object[] { new CreateSubjectTypeCommand { Title="" } };
            yield return new object[] { new CreateSubjectTypeCommand { Title=null } };
            yield return new object[] { new CreateSubjectTypeCommand { Title=StringExtensions.GenerateRandomPersianString(2) } };
            yield return new object[] { new CreateSubjectTypeCommand { Title=StringExtensions.GenerateRandomPersianString(31) } };
            yield return new object[] { new CreateSubjectTypeCommand { Title=StringExtensions.GenerateRandomEnglishString() } };
        }

        IEnumerator IEnumerable.GetEnumerator () => GetEnumerator();
    }

}

