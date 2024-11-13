using Alza.Contracts.DataObjects.Products;
using Alza.Core.Models;

namespace Alza.Appllication.Services;

public interface IProductService
{
    Task<IEnumerable<ProductListItemResponse>> GetListAsync();
    Task<PagedList<ProductListItemResponse>> GetPagedAsync(PagedRequest request);
    Task<ProductResponse> GetByIdAsync(int id);
    Task<ProductResponse> CreateAsync(ProductCreateModel model);
    Task<ProductResponse> UpdateAsync(ProductEditModel model, int id);
}
