using System.Net;
using BigPurpleBank.Api.Product.Common.Exceptions;
using BigPurpleBank.Api.Product.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BigPurpleBank.Api.Product.Common.Middleware;

/// <summary>
/// Exception handler middleware to sanitize exceptions from the API
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex is not ApiProductException)
            {
                _logger.LogError(ex, "Unexpected error occured. Message: {ErrorMessage}", ex.Message);
            }

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            ApiProductException ex => (int)ex.HttpStatusCode,
            _ => (int)HttpStatusCode.InternalServerError
        };


        var responseBody = exception switch
        {
            ApiProductException ex => new ErrorResponse { Errors = ex.Errors },
            _ => new ErrorResponse
            {
                Errors = new List<Error>
                {
                    new()
                    {
                        Code = "urn:au-cds:error:cds-all:Internal/InternalServerError",
                        Title = "Internal Server Error",
                        Detail = "An internal server error has occurred."
                    }
                }
            }
        };

        await context.Response.WriteAsync(responseBody.ToString());
    }
}