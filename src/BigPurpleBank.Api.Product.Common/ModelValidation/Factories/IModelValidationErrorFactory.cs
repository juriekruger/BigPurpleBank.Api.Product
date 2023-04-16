using BigPurpleBank.Api.Product.Common.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.Factories;

/// <summary>
///   Process Model state validation errors. This is used to convert the model state errors into a list of Error models
/// </summary>
public interface IModelValidationErrorFactory : IApiCommonService
{
    /// <summary>
    ///     Process Model state validation errors
    /// </summary>
    /// <param name="contextModelState"></param>
    /// <returns>List of Error models</returns>
    IReadOnlyCollection<Error> ProcessModelState(
        ModelStateDictionary contextModelState);
}