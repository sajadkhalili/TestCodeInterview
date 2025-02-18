using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSF.Core.Domain.ValueObjects;
using SSF.Surveys.Domain.SubjectTypes.Entities;
using SSF.Surveys.EF.Commands.Common;
using Surveys.Provider.Api;
using Xunit.Abstractions;
namespace SSF.Surveys.EndPoint.Api.Test.commons;



[Collection("IntegrationTestCollection")]
public class SurveyFixture : IDisposable
{
    public SubjectTypeClient SubjectTypeClient { get; private set; }
    public HttpClient HttpClient { get; private set; }
    public WebApplicationFactory<Program> Factory { get; private set; }
    public SurveyContext DbContext { get; private set; }
    public SubjectClient SubjectClient { get; private set; }

  
    public SurveyFixture (  )
    {
       
        // راه‌اندازی WebApplicationFactory
        Factory=new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {

                //  builder.ConfigureLogging(logging =>
                //{
                //    logging.ClearProviders();
                //    logging.AddProvider(new Microsoft.Extensions.Logging.x
                //XUnitLoggerProvider(() => TestOutputHelperAccessor.Current));
                //    logging.SetMinimumLevel(LogLevel.Debug);
                //    // You can override the logging configuration if needed
                //    //builder.SetMinimumLevel(LogLevel.Trace);
                //    //builder.AddFilter(_ => true);

                //    // Register the xUnit logger provider
                //    logging.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(_testOutputHelper, appendScope: false));
                //});

                builder.UseEnvironment("Test"); // محیط تست
                                                //builder.ConfigureServices(services =>
                                                //{
                                                //    // حذف دیتابیس واقعی
                                                //    var descriptor = services.SingleOrDefault(
                                                //        d => d.ServiceType==typeof(DbContextOptions<SurveyContext>));

                                                //builder.ConfigureLogging(logging =>
                                                //{
                                                //    logging.ClearProviders();
                                                //    logging.AddProvider(new XUnitLoggerProvider(_output)); // اینجا لاگ‌ها به xUnit متصل می‌شوند
                                                //    logging.SetMinimumLevel(LogLevel.Debug);
                                                //});

                //    if (descriptor!=null)
                //        services.Remove(descriptor);

                //    // افزودن دیتابیس InMemory
                //    services.AddDbContext<SurveyContext>(options =>
                //    {
                //        options.UseInMemoryDatabase("SurveyMemoryDatabase");
                //    });

                //    var serviceProvider = services.BuildServiceProvider();
                //     var db = serviceProvider.GetRequiredService<SurveyContext>();

                //    var d = db.Database.GetConnectionString();

                //    //    // Seed Data (اختیاری)

                //    //    using var scope = serviceProvider.CreateScope();
                //    //    var scopedServices = scope.ServiceProvider;
                //    //    var db = scopedServices.GetRequiredService<SurveyContext>();

                //    //    db.Database.EnsureCreated();
                //    //    SeedDatabase(db);
                //});
            });
        DbContext=Factory.Services.GetRequiredService<SurveyContext>();
        HttpClient=Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect=false
        });

        Initialize();

    }

    private void Initialize ()
    {
        SubjectTypeClient=new SubjectTypeClient(HttpClient);
        SubjectClient=new SubjectClient(HttpClient);
        InsertData.CreateInstance(this);
    }

    private void SeedDatabase ( SurveyContext db )
    {
        // افزودن داده‌های اولیه به دیتابیس
        db.SubjectTypes.Add(new SubjectType(new PersianString(" 1 موضوع")));
        db.SubjectTypes.Add(new SubjectType(new PersianString(" 2 موضوع")));
        db.SaveChanges();
    }

    public void Dispose ()
    {
        // آزادسازی منابع
        HttpClient.Dispose();
        Factory.Dispose();
    }
}
[Collection("IntegrationTestCollection")]
public abstract class BaseTest
{
    protected readonly SurveyFixture surveyFixture;

    protected BaseTest ( SurveyFixture surveyFixture )
    {
        this.surveyFixture=surveyFixture;

    }
}

