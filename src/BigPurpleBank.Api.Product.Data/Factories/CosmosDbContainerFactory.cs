using BigPurpleBank.Api.Product.Common.Model.Config;
using Microsoft.Azure.Cosmos;

namespace BigPurpleBank.Api.Product.Data.Factories;

public class CosmosDbContainerFactory : ICosmosDbContainerFactory
{
    private readonly List<ContainerInfo> _containers;

    /// <summary>
    ///     Azure Cosmos DB Client
    /// </summary>
    private readonly CosmosClient _cosmosClient;

    private readonly string _databaseName;

    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="cosmosClient"></param>
    /// <param name="databaseName"></param>
    /// <param name="containers"></param>
    public CosmosDbContainerFactory(
        CosmosClient cosmosClient,
        string databaseName,
        List<ContainerInfo> containers)
    {
        _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
        _containers = containers ?? throw new ArgumentNullException(nameof(containers));
        _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
    }

    public async Task<ICosmosDbContainer> GetContainer(
        string containerName)
    {
        var container = _containers.FirstOrDefault(x => x.Name == containerName);
        if (container == null)
        {
            throw new ArgumentException($"Unable to find container: {containerName}");
        }


        var dbContainer = new CosmosDbContainer(_cosmosClient, _databaseName, container);
        await dbContainer.GetContainerAsync();
        return dbContainer;
    }
}