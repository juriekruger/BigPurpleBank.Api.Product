using BigPurpleBank.Api.Product.Common.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

/// <summary>
/// Generic interface for field processors. This is used to convert a field validation error into an Error model
/// </summary>
public interface IFieldProcessor
{
    bool CanProcess(
        Type? type);

    IEnumerable<Error> Process(
        ModelErrorCollection valueErrors);
}