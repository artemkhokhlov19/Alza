using Alza.Appllication.Services;
using Alza.Contracts.DataObjects.Products;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Host.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("v{v:apiVersion}/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    //docs

    public ProductsController(IProductService productService)
    {
        this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public virtual async Task<IActionResult> GetListAsync()
    {
        var result = await productService.GetListAsync();
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var item = await productService.GetByIdAsync(id);
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public async Task<IActionResult> PostAsync(ProductCreateModel product)
    {
        var result = await productService.CreateAsync(product);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public async Task<IActionResult> PutAsync(ProductEditModel product, int id)
    {
        var result = await productService.UpdateAsync(product, id);
        return Ok(result);
    }
}
