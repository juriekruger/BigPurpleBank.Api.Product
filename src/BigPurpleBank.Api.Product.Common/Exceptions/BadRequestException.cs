using System.Net;
using BigPurpleBank.Api.Product.Common.Model;

namespace BigPurpleBank.Api.Product.Common.Exceptions;

public class BadRequestException : ApiProductException
{
    public BadRequestException(
        IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        Errors = errors.ToList();
    }

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}