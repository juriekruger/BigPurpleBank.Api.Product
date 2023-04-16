using BigPurpleBank.Api.Product.Common.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BigPurpleBank.Api.Product.Common.ModelValidation.Factories;

public interface IErrorFactory : IApiCommonService
{
    /// <summary>
    ///     Process Model state validation errors
    /// </summary>
    /// <param name="contextModelState"></param>
    /// <returns>List of Error models</returns>
    IReadOnlyCollection<Error> ProcessModelState(
        ModelStateDictionary contextModelState);
}