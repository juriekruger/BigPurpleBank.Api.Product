using BigPurpleBank.Api.Product.Common.Extensions;
using BigPurpleBank.Api.Product.Common.Model.Config;
using BigPurpleBank.Api.Product.Model.Dto;
using BigPurpleBank.Api.Product.Services.Extensions;
using BigPurpleBank.Api.Product.Web.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
    .AddEnvironmentVariables();

builder.Services.Configure<VersionConfig>(builder.Configuration.GetSection("VersionConfig"));

builder.Host.UseSerilog((
    hostContext,
    services,
    loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(builder.Configuration)
        .WriteTo.Console(outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level:u3} {Host} {InstanceId} {SourceContext} {Message:lj}{NewLine}{Exception}");

    if (!builder.Environment.IsDevelopment())
    {
        loggerConfig.WriteTo.ApplicationInsights(services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces);
    }
});
builder.Services.AddControllers();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services
    .AddAutoMapper(typeof(BaseDto))
    .AddSwaggerConfiguration()
    .AddCommonServices()
    .AddApiServices(builder.Configuration);

var app = builder.Build();
app.UseSwaggerConfiguration()
    .UseHttpsRedirection()
    .AddCommonServices();
app.MapControllers();
app.Run();

public partial class Program
{
}