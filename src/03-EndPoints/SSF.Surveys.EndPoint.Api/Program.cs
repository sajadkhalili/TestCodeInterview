
using Asp.Versioning;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using SSF.EFCore;
using SSF.EFCore.AuditLogs;
using SSF.Infrastructure.Abstractions.Serializers;
using SSF.Infrastructure.Implementations.Caching;
using SSF.Infrastructure.Implementations.EndPoints.Exceptions;
using SSF.Surveys.EF.Commands.Common;
using System.Globalization;
using System.Text.Json;
using SSF.Infrastructure.Implementations.Logs.SerilogRegistrations.Extensions.DependencyInjection;
namespace SSF.Surveys.EndPoint.Api
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.ConfigureServices();

            var app = builder.Build();

            app.ConfigurePipeline();
          
         

            app.Run();
        }
    }
}
