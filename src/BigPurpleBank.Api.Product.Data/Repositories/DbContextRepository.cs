using BigPurpleBank.Api.Product.Data.Factories;
using BigPurpleBank.Api.Product.Model.Dto;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace BigPurpleBank.Api.Product.Data.Repositories;

public abstract class DbContextRepository<TEntity> : IDbContextRepository<TEntity>, IContainerContext<TEntity>
    where TEntity : BaseDto
{
    private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;

    protected DbContextRepository(
        ICosmosDbContainerFactory cosmosDbContainerFactory)
    {
        _cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(cosmosDbContainerFactory));
    }

    /// <summary>
    ///     Name of the CosmosDB container
    /// </summary>
    public abstract string ContainerName { get; }

    /// <summary>
    ///     Generate id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract string GenerateId(
        TEntity entity);


    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetItemsAsync(
        IQueryable<TEntity> query,
        int pageSize,
        int page,
        CancellationToken cancellationToken)
    {
        query = query
            .Skip(pageSize * (page - 1))
            .Take(pageSize);
        var resultSetIterator = query.ToFeedIterator();
        var results = new List<TEntity>();
        while (resultSetIterator.HasMoreResults)
        {
            var response = await resultSetIterator.ReadNextAsync(cancellationToken);

            results.AddRange(response.ToList());
        }

        return results;
    }

    /// <inheritdoc />
    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        var container = await GetContainerAsync();
        return container.GetItemLinqQueryable<TEntity>();
    }

    /// <inheritdoc />
    public async Task AddItemAsync(
        TEntity item)
    {
        item.Id = GenerateId(item);
        var container = await GetContainerAsync();
        await container.CreateItemAsync(item);
    }

    public async Task<int> GetRecordCountAsync(
        CancellationToken cancellationToken)
    {
        var container = await GetContainerAsync();
        var result = await container.GetItemQueryIterator<int>("SELECT VALUE COUNT(1) FROM c").ReadNextAsync(cancellationToken);
        return result.FirstOrDefault();
    }

    private async Task<Container> GetContainerAsync()
    {
        var container = await _cosmosDbContainerFactory.GetContainer(ContainerName);
        return await container.GetContainerAsync();
    }
}