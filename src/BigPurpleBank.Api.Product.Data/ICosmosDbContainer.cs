using Microsoft.Azure.Cosmos;

namespace BigPurpleBank.Api.Product.Data;

public interface ICosmosDbContainer
{
    /// <summary>
    ///     Azure Cosmos DB Container
    /// </summary>
    Container Container { get; }
}