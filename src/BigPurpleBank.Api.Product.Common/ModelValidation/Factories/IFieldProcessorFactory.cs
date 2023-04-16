using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.Factories;

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