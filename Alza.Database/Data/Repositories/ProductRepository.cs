using Alza.Database.Context;
using Alza.Database.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alza.Database.Data.Repositories;

public class ProductRepository(IProductDbContext unitOfWork) : BaseRepository<ProductEntity, int>(unitOfWork), IProductRepository
{
    protected override DbSet<ProductEntity> GetDbSet()
    {
        return Context.Products;
    }
}
