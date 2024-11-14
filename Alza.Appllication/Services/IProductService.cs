using Alza.Contracts.DataObjects.Products;
using Alza.Core.Models;

namespace Alza.Appllication.Services;

public interface IProductService
{
    /// <summary>
    /// Get products list.
    /// </summary>
    /// <returns>List of products.</returns>
    Task<BusinessActionResult<IEnumerable<ProductListItemResponse>>> GetListAsync();

    /// <summary>
    /// Get paged products list.
    /// </summary>
    /// <param name="request">Pagination request <see cref="PagedRequest"/></param>
    /// <returns>Paged list of products.</returns>
    Task<BusinessActionResult<PagedList<ProductListItemResponse>>> GetPagedAsync(PagedRequest request);

    /// <summary>
    /// Get product by id.
    /// </summary>
    /// <param name="id">Product id.</param>
    /// <returns>Product.</returns>
    Task<BusinessActionResult<ProductResponse>> GetByIdAsync(int id);

    /// <summary>
    /// Creates new product from model.
    /// </summary>
    /// <param name="model">Product create request.</param>
    /// <returns>Created product.</returns>
    Task<BusinessActionResult<ProductResponse>> CreateAsync(ProductCreateModel model);

    /// <summary>
    /// Updates product description.
    /// </summary>
    /// <param name="model">Product edit request.</param>
    /// <param name="id">Products Id.</param>
    /// <returns>Updated product.</returns>
    Task<BusinessActionResult<ProductResponse>> UpdateDescriptionAsync(ProductEditModel model, int id);
}
