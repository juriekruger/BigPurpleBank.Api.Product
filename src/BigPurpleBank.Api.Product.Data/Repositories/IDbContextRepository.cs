﻿using BigPurpleBank.Api.Product.Model.Dto;

namespace BigPurpleBank.Api.Product.Data.Repositories;

/// <summary>
/// Generic repository for DbContext
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IDbContextRepository<TEntity>
    where TEntity : BaseDto

{
    /// <summary>
    ///     Get items given a query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="pageSize"></param>
    /// <param name="page"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetItemsAsync(
        IQueryable<TEntity> query,
        int pageSize,
        int page,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Get a queryable
    /// </summary>
    /// <returns></returns>
    Task<IQueryable<TEntity>> GetQueryableAsync();

    /// <summary>
    ///     Add an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task AddItemAsync(
        TEntity item);
    
    /// <summary>
    /// Returns the total record count
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetRecordCountAsync(
        CancellationToken cancellationToken);
}