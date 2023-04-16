using BigPurpleBank.Api.Product.Data.Factories;
using BigPurpleBank.Api.Product.Model.Dto;

namespace BigPurpleBank.Api.Product.Data.Repositories;

/// <inheritdoc />
public class ProductRepository : DbContextRepository<ProductDto>, IProductRepository
{
    public ProductRepository(
        ICosmosDbContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory)
    {
    }

    public override string ContainerName => "Products";

    /// <summary>
    ///     Generate Id.
    ///     e.g. "Loans:783dfe25-7ece-4f0b-885e-c0ea72135942"
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public override string GenerateId(
        ProductDto entity) => $"{entity.ProductCategory}:{Guid.NewGuid()}";
}