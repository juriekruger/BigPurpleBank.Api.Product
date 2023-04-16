using BigPurpleBank.Api.Product.Common.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

/// <summary>
/// Base class for field processors. This class is used to convert a field validation error into an Error model
/// </summary>
public abstract class BaseProcessor
{
    protected abstract string Code { get; }
    protected abstract string Title { get; }

    public IEnumerable<Error> Process(
        ModelErrorCollection valueErrors) => valueErrors != null ? valueErrors.Select(GenerateError) : Enumerable.Empty<Error>();

    private Error GenerateError(
        ModelError modelError)
        => new()
        {
            Code = Code,
            Title = Title,
            Detail = modelError.ErrorMessage
        };
}