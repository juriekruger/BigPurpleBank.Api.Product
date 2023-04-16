using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Requests;
using BigPurpleBank.Api.Product.Model.Responses.Product;

namespace BigPurpleBank.Api.Product.Services.Product;

/// <summary>
/// Service for Product
/// </summary>
public interface IProductService : IApiService
{
    /// <summary>
    /// Returns a list of products
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<BaseResponseModel<IEnumerable<ProductViewModel>>> GetAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken);
}