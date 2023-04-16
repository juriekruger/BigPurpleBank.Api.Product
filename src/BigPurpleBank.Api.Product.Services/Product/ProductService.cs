using AutoMapper;
using BigPurpleBank.Api.Product.Common.Exceptions;
using BigPurpleBank.Api.Product.Common.Extensions;
using BigPurpleBank.Api.Product.Common.Model;
using BigPurpleBank.Api.Product.Data.Repositories;
using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Dto;
using BigPurpleBank.Api.Product.Model.Enum;
using BigPurpleBank.Api.Product.Model.Requests;
using BigPurpleBank.Api.Product.Model.Responses.Product;
using BigPurpleBank.Api.Product.Services.MetaData;

namespace BigPurpleBank.Api.Product.Services.Product;

/// <inheritdoc />
public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IResponseMetaDataBuilder _responseMetaDataBuilder;

    public ProductService(
        IProductRepository productRepository,
        IResponseMetaDataBuilder responseMetaDataBuilder,
        IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _responseMetaDataBuilder = responseMetaDataBuilder ?? throw new ArgumentNullException(nameof(responseMetaDataBuilder));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc />
    public async Task<BaseResponseModel<IEnumerable<ProductViewModel>>> GetAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken)
    {
        var recordCount = await _productRepository.GetRecordCountAsync(cancellationToken);
        var metaData = _responseMetaDataBuilder.BuildMetaDataAsync(request.PageSize.GetValueOrDefault(), recordCount);

        ValidatePageSize(request, metaData);

        var query = await _productRepository.GetQueryableAsync();
        query = AddProductCategoryFilter(request, query);
        query = AddBrandFilter(request, query);
        query = AddUpdatedSinceFilter(request, query);
        query = AddEffectiveFilter(request, query);
        query = query.OrderBy(x => x.Name);

        var products = await _productRepository.GetItemsAsync(query, request.PageSize.GetValueOrDefault(), request.Page.GetValueOrDefault(), cancellationToken);
        var response = new BaseResponseModel<IEnumerable<ProductViewModel>>
        {
            Data = _mapper.Map<IList<ProductViewModel>>(products),
            Links = _responseMetaDataBuilder.BuildLinksAsync(request.Page.GetValueOrDefault(), request.PageSize.GetValueOrDefault(), recordCount),
            Meta = metaData
        };
        return response;
    }

    private static IQueryable<ProductDto> AddEffectiveFilter(
        GetProductsRequest request,
        IQueryable<ProductDto> query)
    {
        var nowUnix = DateTime.Now.ToUnixTime();
        query = request.Effective switch
        {
            EffectiveFrom.Current => query.Where(x => x.EffectiveFromUnix <= nowUnix),
            EffectiveFrom.Future => query.Where(x => x.EffectiveFromUnix > nowUnix),
            _ => query
        };
        return query;
    }

    private static IQueryable<ProductDto> AddUpdatedSinceFilter(
        GetProductsRequest request,
        IQueryable<ProductDto> query)
    {
        if (!request.UpdatedSince.HasValue)
        {
            return query;
        }

        var updatedSinceUnix = request.UpdatedSince.Value.ToUnixTime();
        query = query.Where(x => x.LastUpdatedUnix <= updatedSinceUnix);

        return query;
    }

    private static IQueryable<ProductDto> AddBrandFilter(
        GetProductsRequest request,
        IQueryable<ProductDto> query)
    {
        if (!string.IsNullOrWhiteSpace(request.Brand))
        {
            query = query.Where(x => x.Brand == request.Brand);
        }

        return query;
    }

    private static IQueryable<ProductDto> AddProductCategoryFilter(
        GetProductsRequest request,
        IQueryable<ProductDto> query)
    {
        if (request.ProductCategory.HasValue)
        {
            query = query.Where(x => x.ProductCategory == request.ProductCategory.Value);
        }

        return query;
    }

    private static void ValidatePageSize(
        BaseRequest request,
        MetaPaginated metaData)
    {
        if (request.PageSize.GetValueOrDefault() > metaData.TotalPages)
        {
            throw new BadRequestException(new[] { new InvalidPageError() });
        }
    }
}