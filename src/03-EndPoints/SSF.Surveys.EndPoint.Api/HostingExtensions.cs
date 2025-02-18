using Microsoft.AspNetCore.Diagnostics;
using SSF.Core.Contracts;
using SSF.Core.Domain.Exceptions;
using SSF.Infrastructure.Implementations.CurrentUsers;
using SSF.Infrastructure.Implementations.IdGenerators;
using SSF.Infrastructure.Implementations.Localizations;
using SSF.Infrastructure.Implementations.Serializers.Microsoft;
using SSF.Mediators.MediatorDelegates;
using System.Text.Json;
using Asp.Versioning;
using SSF.EFCore;
using SSF.EFCore.AuditLogs;
using SSF.Infrastructure.Implementations.Caching;
using SSF.Infrastructure.Implementations.Logs.SerilogRegistrations.Extensions.DependencyInjection;
using SSF.Surveys.EF.Commands.Common;
using Microsoft.AspNetCore.Localization;
using SSF.Infrastructure.Abstractions.Serializers;
using System.Globalization;

namespace SSF.Surveys.EndPoint.Api;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices ( this WebApplicationBuilder builder )
    {
        builder.AddSSFSerilogServices2();

        builder.Services.AddLocalization(options => { options.ResourcesPath="Resources"; }

        );
        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy=JsonNamingPolicy.CamelCase;
            })
            ;
        builder.Services.AddServiceExternalAuditLog(p =>
        {
            p.ConnectionString=builder.Configuration.GetConnectionString("LogDB");
        });
        builder.Services.AddDbContextPool<SurveyContext>(
            ( provider, options ) =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Survey"))
                    .AddInterceptors(provider.GetRequiredService<ExternalAuditLogInterceptor>())
                    .UseAuditLogging(enableAuditLog: true);
            }
        );

        builder.Services.AddSsfCacheService(builder.Configuration);

        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApiDocument();

        builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion=new ApiVersion(1);
                options.ReportApiVersions=true;
                options.AssumeDefaultVersionWhenUnspecified=true;
                options.ApiVersionReader=new UrlSegmentApiVersionReader();
                //options.ApiVersionReader=ApiVersionReader.Combine(
                //    new UrlSegmentApiVersionReader(),

                //    new HeaderApiVersionReader("X-Api-Version"));
            })

            .AddApiExplorer(options =>
                {
                    options.GroupNameFormat="'v'V";
                    options.SubstituteApiVersionInUrl=true;
                }
            );

        builder.Services.AddControllers(op =>
        {
           
        }

      )

          .AddJsonOptions(options =>
          {
              options.JsonSerializerOptions.PropertyNamingPolicy=JsonNamingPolicy.CamelCase; 
              options.JsonSerializerOptions.DictionaryKeyPolicy=JsonNamingPolicy.CamelCase;
          })

  

            ;
        builder.Services.AddDynamicLocalizationProvider(options =>
        {
            options.ResourceType=typeof(SharedResource);

        });
        builder.Services.AddScoped<IIdGenerator, IdGeneratorSnowflake>();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter=true;
        });
        builder.Services.AddMediatorDelegate("SSF").WithDefaultBehavior();
     

     
        builder.Services.AddCustomeDepenecies(optin => optin.AssmblyNamesForLoad="SSF");
        builder.Services.AddFakeUserInfoService();
        builder.Services.AddMicrosoftSerializer();
       
        return builder;
    }
    public static WebApplication ConfigurePipeline ( this WebApplication app )
    {
        app.UseExceptionHandler();

        SSFJson.Init(app.Services.GetRequiredService<IJsonSerializer>());

        if (app.Environment.IsDevelopment())
        {
            // Add OpenAPI 3.0 document serving middleware
            // Available at: http://localhost:<port>/swagger/v1/swagger.json
            app.UseOpenApi();

            // Add web UIs to interact with the document
            // Available at: http://localhost:<port>/swagger
            app.UseSwaggerUi(); // UseSwaggerUI Protected by if (env.IsDevelopment())
        }

        var supportedCultures = new[] { "en", "fa" };
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture=new RequestCulture("en"),
            SupportedCultures=supportedCultures.Select(c => new CultureInfo(c)).ToList(),
            SupportedUICultures=supportedCultures.Select(c => new CultureInfo(c)).ToList()
        };

        app.UseRequestLocalization(localizationOptions);

        //   app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthorization();
        // app.MapControllers();
        app.MapAreaControllerRoute(
            name: "areaRoute",
            areaName: "Surveys",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
            name: "defaultRoute",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
        return app;
    }
}
