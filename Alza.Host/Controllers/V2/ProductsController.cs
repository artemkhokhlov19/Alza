using Alza.Appllication.Services;
using Alza.Contracts.DataObjects.Products;
using Alza.Core.Models;
using Alza.Host.Controllers.V1;
using Alza.Host.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Host.Controllers.V2;

[ApiController]
[ApiVersion("2")]
[Route("v{v:apiVersion}/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    /// <summary>
    /// Retrieves paginated list of products from source.
    /// </summary>
    /// <param name="request">Pagination request.</param>
    /// <returns>ingle page of products accprding to request.</returns>
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public async Task<IActionResult> GetListAsync([FromQuery] PagedRequest request)
    {
        var result = await productService.GetPagedAsync(request);
        return result.ToActionResult();
    }
}
