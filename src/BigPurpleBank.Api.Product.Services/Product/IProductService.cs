using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Requests;
using BigPurpleBank.Api.Product.Model.Responses.Product;

namespace BigPurpleBank.Api.Product.Services.Product;

public interface IProductService : IApiService
{
    Task<BaseResponseModel<IEnumerable<ProductViewModel>>> GetAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken);
}