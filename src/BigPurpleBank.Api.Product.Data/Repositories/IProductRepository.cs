using BigPurpleBank.Api.Product.Model.Dto;

namespace BigPurpleBank.Api.Product.Data.Repositories;

public interface IProductRepository : IDbContextRepository<ProductDto>
{
}