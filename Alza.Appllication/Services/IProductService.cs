using Alza.Contracts.DataObjects.Products;
using Alza.Core.Models;

namespace Alza.Appllication.Services;

public interface IProductService
{
    Task<IList<ProductListItemResponse>> GetListAsync();
    Task<ActionResult<ProductResponse>> GetByIdAsync(int id);
    Task<ActionResult<ProductResponse>> CreateAsync(ProductEditModel model);
    Task<ActionResult<ProductResponse>> UpdateAsync(ProductEditModel model, int id);
}
