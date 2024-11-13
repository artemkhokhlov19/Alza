using Alza.Appllication.Services;
using Alza.Contracts.DataObjects.Products;
using Alza.Host.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Host.Controllers.V1;

/// <summary>
/// Controller for products.
/// </summary>
[ApiController]
[ApiVersion("1")]
[Route("v{v:apiVersion}/[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="productService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ProductsController(IProductService productService)
    {
        this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    /// <summary>
    /// Retrieves list of products from source.
    /// </summary>
    /// <returns>A list of products.</returns>
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public virtual async Task<IActionResult> GetListAsync()
    {
        var result = await productService.GetListAsync();
        return result.ToActionResult();
    }

    /// <summary>
    /// Retrieves product by its id.
    /// </summary>
    /// <param name="id">The ID of product to retrieve.</param>
    /// <returns>Requested product.</returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var item = await productService.GetByIdAsync(id);
        return item.ToActionResult();
    }

    /// <summary>
    /// Creates new product.
    /// </summary>
    /// <param name="product">Product creation request.</param>
    /// <returns>Created product.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public async Task<IActionResult> PostAsync(ProductCreateModel product)
    {
        var result = await productService.CreateAsync(product);
        return result.ToActionResult();
    }

    /// <summary>
    /// Updates description od product with given id.
    /// </summary>
    /// <param name="product">Product edit request.</param>
    /// <param name="id">ID of product to update.</param>
    /// <returns>Update product.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductListItemResponse>))]
    public async Task<IActionResult> UpdateDescriptionAsync(ProductEditModel product, int id)
    {
        var result = await productService.UpdateAsync(product, id);
        return result.ToActionResult();
    }
}
