using AutoMapper;
using BigPurpleBank.Api.Product.Common.Extensions;
using BigPurpleBank.Api.Product.Data.Repositories;
using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Enum;
using BigPurpleBank.Api.Product.Model.Requests;
using BigPurpleBank.Api.Product.Model.Responses.Product;

namespace BigPurpleBank.Api.Product.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponseModel<IEnumerable<ProductViewModel>>> GetAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken)
    {
        var query = await _productRepository.GetQueryableAsync();
        if (request.ProductCategory.HasValue)
        {
            query = query.Where(x => x.ProductCategory == request.ProductCategory.Value);
        }
        
        if (!string.IsNullOrWhiteSpace(request.Brand))
        {
            query = query.Where(x => x.Brand == request.Brand);
        }
        
        if (request.UpdatedSince.HasValue)
        {
            var updatedSinceUnix = request.UpdatedSince.Value.ToUnixTime();
            query = query.Where(x => x.LastUpdatedUnix <= updatedSinceUnix);
        }
        
        var nowUnix = DateTime.Now.ToUnixTime();
        switch (request.Effective)
        {
            case EffectiveFrom.Current:
                query = query.Where(x => x.EffectiveFromUnix <= nowUnix);
                break;
            case EffectiveFrom.Future:
                query = query.Where(x => x.EffectiveFromUnix > nowUnix);
                break;
        }

        var products = await _productRepository.GetItemsAsync(query);

        return new BaseResponseModel<IEnumerable<ProductViewModel>>
        {
            Data = _mapper.Map<IList<ProductViewModel>>(products)
        };
    }
}