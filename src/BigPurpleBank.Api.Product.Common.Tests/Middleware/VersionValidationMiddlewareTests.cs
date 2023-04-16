using System.Net;
using System.Text.Json;
using BigPurpleBank.Api.Product.Common.Constants;
using BigPurpleBank.Api.Product.Common.Middleware;
using BigPurpleBank.Api.Product.Common.Model;
using BigPurpleBank.Api.Product.Common.Model.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shouldly;

namespace BigPurpleBank.Api.Product.Common.Tests.Middleware;

public class VersionValidationMiddlewareTests : IDisposable
{
    private readonly IHost _host;

    public VersionValidationMiddlewareTests()
    {
        _host = new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services => { services.AddSingleton(Options.Create(new VersionConfig { SupportedVersions = new[] { 3 } })); })
                    .Configure(app =>
                    {
                        app.UseMiddleware<VersionValidationMiddleware>();
                        app.Map("/test", appBuilder => { appBuilder.Run(async context => { await context.Response.WriteAsync("OK"); }); });
                    });
            })
            .Start();
    }

    public void Dispose()
    {
        _host.Dispose();
    }

    [Theory]
    [InlineData("fake", HttpStatusCode.BadRequest, "urn:au-cds:error:cds-all:Header/InvalidVersion", "Invalid Version", "The version provided is not valid.")]
    [InlineData("-1", HttpStatusCode.BadRequest, "urn:au-cds:error:cds-all:Header/InvalidVersion", "Invalid Version", "The version provided is not valid.")]
    [InlineData("0", HttpStatusCode.BadRequest, "urn:au-cds:error:cds-all:Header/InvalidVersion", "Invalid Version", "The version provided is not valid.")]
    [InlineData("1", HttpStatusCode.NotAcceptable, "urn:au-cds:error:cds-all:Header/UnsupportedVersion", "Unsupported Version", "The version provided is not supported.")]
    [InlineData("2", HttpStatusCode.NotAcceptable, "urn:au-cds:error:cds-all:Header/UnsupportedVersion", "Unsupported Version", "The version provided is not supported.")]
    public async Task InvokeAsync_WhenCalled_ReturnsExpectedResult(
        string version,
        HttpStatusCode expectedHttpStatusCode,
        string expectedCode,
        string expectedTitle,
        string expectedDetail)
    {
        var client = _host.GetTestClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/test");
        request.Headers.Add(HeaderNames.Version, version);
        var response = await client.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseString);

        response.StatusCode.ShouldBe(expectedHttpStatusCode);
        errorResponse.ShouldNotBeNull();
        errorResponse.Errors.ShouldNotBeNull();
        errorResponse.Errors.Count.ShouldBe(1);
        errorResponse.Errors[0].Code.ShouldBe(expectedCode);
        errorResponse.Errors[0].Title.ShouldBe(expectedTitle);
        errorResponse.Errors[0].Detail.ShouldBe(expectedDetail);
    }

    [Fact]
    public async Task InvokeAsync_WhenCalledWithSupportedVersion_ReturnsOk()
    {
        var client = _host.GetTestClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/test");
        request.Headers.Add(HeaderNames.Version, "3");
        var response = await client.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        responseString.ShouldBe("OK");
    }
}