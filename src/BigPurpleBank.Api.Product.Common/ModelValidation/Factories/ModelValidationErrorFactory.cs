using BigPurpleBank.Api.Product.Common.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.Factories;

/// <inheritdoc />
public class ModelValidationErrorFactory : IModelValidationErrorFactory
{
    private readonly IFieldProcessorFactory _processorFactory;

    public ModelValidationErrorFactory(
        IFieldProcessorFactory processorFactory)
    {
        _processorFactory = processorFactory;
    }

    /// <inheritdoc />
    public IReadOnlyCollection<Error> ProcessModelState(
        ModelStateDictionary contextModelState)
    {
        var errors = new List<Error>();
        foreach (var stateEntry in contextModelState)
        {
            var fieldProcessor = _processorFactory.Get(stateEntry.Value.RawValue?.GetType());
            errors.AddRange(fieldProcessor.Process(stateEntry.Value.Errors));
        }

        return errors;
    }
}