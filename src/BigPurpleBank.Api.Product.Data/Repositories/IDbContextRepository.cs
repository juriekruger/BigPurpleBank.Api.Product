using BigPurpleBank.Api.Product.Model.Dto;

namespace BigPurpleBank.Api.Product.Data.Repositories;

public interface IDbContextRepository<TEntity>
    where TEntity : BaseDto

{
    /// <summary>
    ///     Get one item by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetItemAsync(string id);
    
    /// <summary>
    ///     Get items given a query
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetItemsAsync(IQueryable<TEntity> query);
    
    /// <summary>
    /// Get a queryable
    /// </summary>
    /// <returns></returns>
    Task<IQueryable<TEntity>> GetQueryableAsync();
    
    /// <summary>
    /// Add an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task AddItemAsync(TEntity item);
}