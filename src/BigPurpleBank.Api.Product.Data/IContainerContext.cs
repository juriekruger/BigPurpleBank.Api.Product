using BigPurpleBank.Api.Product.Model.Dto;
using Microsoft.Azure.Cosmos;

namespace BigPurpleBank.Api.Product.Data;

/// <summary>
///  Defines the container level context
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IContainerContext<TEntity> where TEntity : BaseDto
{
    string ContainerName { get; }
    string GenerateId(TEntity entity);
}