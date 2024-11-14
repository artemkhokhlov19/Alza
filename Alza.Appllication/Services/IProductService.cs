using Alza.Contracts.DataObjects.Products;
using Alza.Core.Models;

namespace Alza.Appllication.Services;

public interface IProductService
{
    Task<BusinessActionResult<IEnumerable<ProductListItemResponse>>> GetListAsync();
    Task<BusinessActionResult<PagedList<ProductListItemResponse>>> GetPagedAsync(PagedRequest request);
    Task<BusinessActionResult<ProductResponse>> GetByIdAsync(int id);
    Task<BusinessActionResult<ProductResponse>> CreateAsync(ProductCreateModel model);
    Task<BusinessActionResult<ProductResponse>> UpdateDescriptionAsync(ProductEditModel model, int id);
}
