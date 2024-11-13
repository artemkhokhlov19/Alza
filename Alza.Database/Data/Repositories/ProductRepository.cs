using Alza.Database.Context;
using Alza.Database.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alza.Database.Data.Repositories;

public class ProductRepository(IProductDbContext unitOfWork) : BaseRepository<ProductEntity, int>(unitOfWork), IProductRepository
{
    public Task<int> GetCountAsync()
    {
        var query = GetQueryable();
        return query.CountAsync();
    }

    public async Task<IEnumerable<ProductEntity>> GetPagedAsync(int offset, int limit)
    {
        var query = GetQueryable();

        query = query.Skip(offset).Take(limit);
        var result = await query.ToArrayAsync();
        return result ?? Enumerable.Empty<ProductEntity>();

    }

    protected override DbSet<ProductEntity> GetDbSet()
    {
        return Context.Products;
    }
}
