using BigPurpleBank.Api.Product.Data.Factories;
using BigPurpleBank.Api.Product.Model.Dto;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace BigPurpleBank.Api.Product.Data.Repositories;

public abstract class DbContextRepository<TEntity> : IDbContextRepository<TEntity>, IContainerContext<TEntity>
    where TEntity : BaseDto
{

    /// <summary>
    ///     Name of the CosmosDB container
    /// </summary>
    public abstract string ContainerName { get; }
    
    
    private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;
    private Container _container;

    protected DbContextRepository(ICosmosDbContainerFactory cosmosDbContainerFactory)
    {
        _cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(cosmosDbContainerFactory));
    }
    
    private async Task InitializeContainer()
    {
        var container = await _cosmosDbContainerFactory.GetContainer(ContainerName);
        _container = container.Container;
    }

    /// <summary>
    ///     Generate id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract string GenerateId(TEntity entity);
    
    
    /// <inheritdoc />
    public async Task<TEntity> GetItemAsync(string id)
    {
        await InitializeContainer();
        try
        {
            ItemResponse<TEntity> response = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetItemsAsync(IQueryable<TEntity> query)
    {
        await InitializeContainer();
        var resultSetIterator = query.ToFeedIterator();
        var results = new List<TEntity>();
        while (resultSetIterator.HasMoreResults)
        {
            var response = await resultSetIterator.ReadNextAsync();

            results.AddRange(response.ToList());
        }

        return results;
    }

    /// <inheritdoc />
    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        await InitializeContainer();
        return _container.GetItemLinqQueryable<TEntity>();
    }
    
    public async Task AddItemAsync(TEntity item)
    {
        await InitializeContainer();
        item.Id = GenerateId(item);
        await _container.CreateItemAsync(item);
    }
}