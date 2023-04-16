using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.Factories;

/// <summary>
/// Returns the correct field processor for the given type. If no processor is found, the default processor is returned
/// </summary>
public interface IFieldProcessorFactory : IApiCommonService
{
    /// <summary>
    ///     Returns a processor that can process a field validation error.
    /// </summary>
    /// <param name="type">Type of the field</param>
    /// <returns></returns>
    IFieldProcessor Get(
        Type? type);
}