using BigPurpleBank.Api.Product.Common.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.FieldProcessors;

public interface IFieldProcessor
{
    bool CanProcess(
        Type? type);

    IEnumerable<Error> Process(
        ModelErrorCollection valueErrors);
}