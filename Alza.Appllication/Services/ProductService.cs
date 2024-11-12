using Alza.Contracts.DataObjects.Products;
using Alza.Core.Models;

namespace Alza.Appllication.Services;

public class ProductService : IProductService
{
    public Task<ActionResult<ProductResponse>> CreateAsync(ProductEditModel model)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<ProductResponse>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<ProductListItemResponse>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<ProductResponse>> UpdateAsync(ProductEditModel model, int id)
    {
        throw new NotImplementedException();
    }
}
