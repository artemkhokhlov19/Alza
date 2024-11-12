using Alza.Core.Models;
using Alza.Database.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alza.Database.Context;

public interface IProductDbContext : IUnitOfWork, IMigrable
{
    public DbSet<ProductEntity> Products { get; set; }
}
