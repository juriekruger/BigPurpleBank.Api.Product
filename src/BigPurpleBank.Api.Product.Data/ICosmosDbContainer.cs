using Microsoft.Azure.Cosmos;

namespace BigPurpleBank.Api.Product.Data;

/// <summary>
/// Gets Azure Cosmos DB Container
/// </summary>
public interface ICosmosDbContainer
{
    /// <summary>
    ///     Initialize a container. If the database does not exist it will get created. If the container does not exist it will
    ///     get created.
    /// </summary>
    Task<Container> GetContainerAsync();
}