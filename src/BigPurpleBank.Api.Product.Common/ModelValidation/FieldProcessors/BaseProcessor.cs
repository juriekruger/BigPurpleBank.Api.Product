using BigPurpleBank.Api.Product.Common.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

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