using Alza.Core.Data;
using Alza.Database.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alza.Database.Context;

public class ProductDbContext(DbContextOptions options) : EntityFrameworkDbContextBase(options), IProductDbContext
{
    public DbSet<ProductEntity> Products { get; set; }
}
