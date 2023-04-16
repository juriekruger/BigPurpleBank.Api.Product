using BigPurpleBank.Api.Product.Common.Filter;
using BigPurpleBank.Api.Product.Common.Model;
using BigPurpleBank.Api.Product.Model;
using BigPurpleBank.Api.Product.Model.Requests;
using BigPurpleBank.Api.Product.Model.Responses;
using BigPurpleBank.Api.Product.Model.Responses.Product;
using BigPurpleBank.Api.Product.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace BigPurpleBank.Api.Product.Web.Controllers.V3;

/// <summary>
///     Controller to handle request relating to bank products
/// </summary>
[Route("v3/banking/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="productService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ProductController(
        IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    /// <summary>
    ///     Obtain a list of products that are currently openly offered to the market
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET /banking/products
    ///     Accept: application/json
    ///     x-v: string
    ///     x-min-v: string
    /// </remarks>
    /// <response code="200">Returns available products</response>
    [HttpGet(Name = "GetProducts")]
    [ServiceFilter(typeof(ValidateModeFilter))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BaseResponseModel<ProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public Task<BaseResponseModel<IEnumerable<ProductViewModel>>> Get(
        [FromQuery] GetProductsRequest request) => _productService.GetAsync(request, Request.HttpContext.RequestAborted);
}