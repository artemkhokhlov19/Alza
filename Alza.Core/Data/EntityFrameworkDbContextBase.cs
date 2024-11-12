using Alza.Core.Models;
using Microsoft.EntityFrameworkCore;
using Alza.Core.Data.Extensions;

namespace Alza.Core.Data;

public abstract class EntityFrameworkDbContextBase : DbContext, IUnitOfWork, IMigrable
{
    protected EntityFrameworkDbContextBase()
    {

    }

    protected EntityFrameworkDbContextBase(DbContextOptions options)
        : base(options)
    {
    }

    public void Commit()
    {
        SaveChanges();
    }

    public void Migrate()
    {
        Database.Migrate();
    }

    public void Rollback()
    {
        this.RollbackChanges();
    }

    
}
