using BigPurpleBank.Api.Product.Model.Dto;

namespace BigPurpleBank.Api.Product.Data;

/// <summary>
///     Defines the container level context
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IContainerContext<in TEntity> where TEntity : BaseDto
{
    string ContainerName { get; }

    string GenerateId(
        TEntity entity);
}