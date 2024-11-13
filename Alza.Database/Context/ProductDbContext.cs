using Alza.Core.Data;
using Alza.Database.Context;
using Alza.Database.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class ProductDbContext : EntityFrameworkDbContextBase, IProductDbContext
{
    protected ProductDbContext()
    {

    }

    public ProductDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<ProductEntity> Products { get; set; }
}