using Alza.Appllication.Services;
using Alza.Contracts.DataObjects.Products;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Host.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{v:apiVersion}/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await productService.GetListAsync();
        return Ok(result);
    }
}
