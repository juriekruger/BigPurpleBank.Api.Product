using BigPurpleBank.Api.Product.Common.Model.Config;
using Microsoft.Azure.Cosmos;

namespace BigPurpleBank.Api.Product.Data;


/// <inheritdoc />
public class CosmosDbContainer : ICosmosDbContainer
{
    private readonly ContainerInfo _containerInfo;
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;

    public CosmosDbContainer(
        CosmosClient cosmosClient,
        string databaseName,
        ContainerInfo containerInfo)
    {
        _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
        _containerInfo = containerInfo ?? throw new ArgumentNullException(nameof(containerInfo));

    }


    /// <inheritdoc />
    public async Task<Container> GetContainerAsync()
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
        var containerResponse = await database.Database.CreateContainerIfNotExistsAsync(
            _containerInfo.Name,
            _containerInfo.PartitionKey,
            400
        );
        return containerResponse.Container;
    }
}