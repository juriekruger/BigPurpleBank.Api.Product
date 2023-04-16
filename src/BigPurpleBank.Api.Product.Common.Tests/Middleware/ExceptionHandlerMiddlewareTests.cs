using System.Net;
using System.Text.Json;
using BigPurpleBank.Api.Product.Common.Exceptions;
using BigPurpleBank.Api.Product.Common.Middleware;
using BigPurpleBank.Api.Product.Common.Model;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Shouldly;

namespace BigPurpleBank.Api.Product.Common.Tests.Middleware;

public class ExceptionHandlerMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_WhenCalled_Exception_ReturnsExpectedResult()
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .Configure(app =>
                    {
                        app.UseMiddleware<ExceptionHandlerMiddleware>();
                        app.Map("/Exception", appBuilder => { appBuilder.Run(context => throw new Exception("Test Exception")); });
                    });
            })
            .StartAsync();

        var client = host.GetTestClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/Exception");
        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);


        response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        errorResponse.ShouldNotBeNull();
        errorResponse.Errors.ShouldNotBeNull();
        errorResponse.Errors.Count.ShouldBe(1);
        errorResponse.Errors[0].ShouldBeEquivalentTo(new Error
        {
            Code = "urn:au-cds:error:cds-all:Internal/InternalServerError",
            Title = "Internal Server Error",
            Detail = "An internal server error has occurred."
        });
    }

    [Fact]
    public async Task InvokeAsync_WhenCalled_ApiProductException_ReturnsExpectedResult()
    {
        var error = new Faker<Error>()
            .RuleFor(e => e.Code, f => f.Random.String2(10))
            .RuleFor(e => e.Title, f => f.Random.String2(10))
            .RuleFor(e => e.Detail, f => f.Random.String2(10))
            .Generate();
        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .Configure(app =>
                    {
                        app.UseMiddleware<ExceptionHandlerMiddleware>();
                        app.Map("/ApiProductException", appBuilder => { appBuilder.Run(context => throw new ApiProductException(new List<Error> { error })); });
                    });
            })
            .StartAsync();

        var client = host.GetTestClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/ApiProductException");
        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);


        response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        errorResponse.ShouldNotBeNull();
        errorResponse.Errors.ShouldNotBeNull();
        errorResponse.Errors.Count.ShouldBe(1);
        errorResponse.Errors[0].ShouldBeEquivalentTo(error);
    }

    [Fact]
    public async Task InvokeAsync_WhenCalled_ApiProductException_Ok()
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .Configure(app =>
                    {
                        app.UseMiddleware<ExceptionHandlerMiddleware>();
                        app.Map("/NoException", appBuilder => { appBuilder.Run(async context => await context.Response.WriteAsync("Ok")); });
                    });
            })
            .StartAsync();

        var client = host.GetTestClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/NoException");
        var response = await client.SendAsync(request);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}