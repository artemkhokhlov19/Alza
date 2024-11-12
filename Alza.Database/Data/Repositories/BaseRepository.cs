using Alza.Core.Data.Repositories;
using Alza.Core.Models;
using Alza.Database.Context;

namespace Alza.Database.Data.Repositories;

public abstract class BaseRepository<TEntity, TKey>(IUnitOfWork unitOfWork) 
    : EntityFrameworkRepositoryBase<TEntity, TKey>(unitOfWork)
    where TEntity : class, IEntity<TKey>
{
    protected IProductDbContext Context => (IProductDbContext)UnitOfWork;
}
