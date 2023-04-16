using BigPurpleBank.Api.Product.Common.Exceptions;
using BigPurpleBank.Api.Product.Common.ModelValidation.Factories;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BigPurpleBank.Api.Product.Common.Filter;

public class ValidateModeFilter : ActionFilterAttribute
{
    private readonly IErrorFactory _errorFactory;

    public ValidateModeFilter(
        IErrorFactory errorFactory)
    {
        _errorFactory = errorFactory;
    }

    public override void OnActionExecuting(
        ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = _errorFactory.ProcessModelState(context.ModelState);
        if (errors.Any())
        {
            throw new BadRequestException(errors);
        }
    }
}