using BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.Factories;

public class FieldProcessorFactory : IFieldProcessorFactory
{
    private readonly IEnumerable<IFieldProcessor> _fieldProcessors;

    public FieldProcessorFactory(
        IEnumerable<IFieldProcessor> fieldProcessors)
    {
        _fieldProcessors = fieldProcessors ?? throw new ArgumentNullException(nameof(fieldProcessors));
    }

    /// <inheritdoc />
    public IFieldProcessor Get(
        Type? type)
    {
        var processor = _fieldProcessors.FirstOrDefault(x => x.CanProcess(type)) ?? new DefaultFieldProcessor();

        return processor;
    }
}