using System.Net;
using BigPurpleBank.Api.Product.Common.Model;

namespace BigPurpleBank.Api.Product.Common.Exceptions;

/// <summary>
/// General API exception used to sanitize exceptions from the API
/// </summary>
public class ApiProductException : Exception
{
    protected ApiProductException()
    {
    }

    public ApiProductException(
        List<Error> errors)
    {
        Errors = errors;
    }

    public virtual HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;
    public List<Error> Errors { get; protected init; } = new();
}