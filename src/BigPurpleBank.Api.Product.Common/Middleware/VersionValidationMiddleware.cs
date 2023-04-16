using System.Net;
using BigPurpleBank.Api.Product.Common.Constants;
using BigPurpleBank.Api.Product.Common.Model;
using BigPurpleBank.Api.Product.Common.Model.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BigPurpleBank.Api.Product.Common.Middleware;

/// <summary>
/// Middleware to validate the version header.
/// If the version header is invalid, it will return BadRequest.
/// If the version header is not supported, it will return NotAcceptable.
/// </summary>
public class VersionValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int[]? _supportedVersions;

    public VersionValidationMiddleware(
        RequestDelegate next,
        IOptions<VersionConfig> options)
    {
        _next = next;
        _supportedVersions = options.Value.SupportedVersions;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ILogger<VersionValidationMiddleware> logger)
    {
        if (!context.Request.Headers.TryGetValue(HeaderNames.Version, out var version) || !int.TryParse(version, out var versionNumber) || versionNumber <= 0)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(new ErrorResponse { Errors = new List<Error> { new InvalidVersionError() } }.ToString());
        }
        else if (_supportedVersions?.Contains(versionNumber) == false)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
            await context.Response.WriteAsync(new ErrorResponse { Errors = new List<Error> { new UnsupportedVersionError() } }.ToString());
        }
        else
        {
            await _next(context);
        }
    }
}