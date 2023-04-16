using BigPurpleBank.Api.Product.Model.Dto;

namespace BigPurpleBank.Api.Product.Data.Repositories;

/// <summary>
/// Repository for Product
/// </summary>
public interface IProductRepository : IDbContextRepository<ProductDto>
{

}