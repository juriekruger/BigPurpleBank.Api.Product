using BigPurpleBank.Api.Product.Common.Model.Config;
using Microsoft.Azure.Cosmos;

namespace BigPurpleBank.Api.Product.Data;

public class CosmosDbContainer : ICosmosDbContainer
{
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;
    private readonly ContainerInfo _containerName;
    public Container Container { get; set; }

    public CosmosDbContainer(CosmosClient cosmosClient,
        string databaseName,
        ContainerInfo containerName)
    {
        _cosmosClient = cosmosClient;
        _databaseName = databaseName;
        _containerName = containerName;

    }

    /// <summary>
    /// Initialize a container. If the database does not exist it will get created. If the container does not exist it will get created.
    /// </summary>
    public async Task InitializeAsync()
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
        var  container = await database.Database.CreateContainerIfNotExistsAsync(
            id: _containerName.Name,
            partitionKeyPath: _containerName.PartitionKey,
            throughput: 400
        );
        Container = container.Container;
    }
}