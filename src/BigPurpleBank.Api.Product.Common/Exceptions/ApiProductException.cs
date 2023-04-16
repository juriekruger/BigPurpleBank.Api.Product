using System.Net;
using BigPurpleBank.Api.Product.Common.Model;

namespace BigPurpleBank.Api.Product.Common.Exceptions;

public class ApiProductException : Exception
{
    public ApiProductException()
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