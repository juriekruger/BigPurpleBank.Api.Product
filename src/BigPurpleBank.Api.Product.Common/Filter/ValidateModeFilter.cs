using BigPurpleBank.Api.Product.Common.Exceptions;
using BigPurpleBank.Api.Product.Common.ModelValidation.Factories;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BigPurpleBank.Api.Product.Common.Filter;

/// <summary>
/// Action filter to validate the model state. If the model state is invalid, it will throw a BadRequestException
/// </summary>
public class ValidateModeFilter : ActionFilterAttribute
{
    private readonly IModelValidationErrorFactory _modelValidationErrorFactory;

    public ValidateModeFilter(
        IModelValidationErrorFactory modelValidationErrorFactory)
    {
        _modelValidationErrorFactory = modelValidationErrorFactory;
    }

    public override void OnActionExecuting(
        ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = _modelValidationErrorFactory.ProcessModelState(context.ModelState);
        if (errors.Any())
        {
            throw new BadRequestException(errors);
        }
    }
}