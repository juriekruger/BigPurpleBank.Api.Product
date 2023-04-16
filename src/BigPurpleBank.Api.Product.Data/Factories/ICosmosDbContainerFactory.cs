namespace BigPurpleBank.Api.Product.Data.Factories;

public interface ICosmosDbContainerFactory
{
    /// <summary>
    ///     Returns a CosmosDbContainer wrapper
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task<ICosmosDbContainer> GetContainer(
        string containerName);
}