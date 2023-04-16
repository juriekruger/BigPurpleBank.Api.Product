using BigPurpleBank.Api.Product.Common.Exceptions;
using BigPurpleBank.Api.Product.Common.Model;
using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Requests;

namespace BigPurpleBank.Api.Product.Services.Product;

public abstract class BaseService
{
    protected static void ValidatePageSize(
        BaseRequest request,
        MetaPaginated metaData)
    {
        if (request.Page.GetValueOrDefault() > metaData.TotalPages)
        {
            throw new BadRequestException(new[] { new InvalidPageError() });
        }
    }
}