using Alza.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Alza.Core.Data.Repositories;

public abstract class EntityFrameworkRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    public IUnitOfWork UnitOfWork { get; }
    
    protected virtual IQueryable<TEntity> GetQueryable()
    {
        return GetDbSet();
    }

    protected abstract DbSet<TEntity> GetDbSet();

    protected EntityFrameworkRepositoryBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task AddAsync(TEntity entity)
    {
        await GetDbSet().AddAsync(entity);
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        var query = this.GetQueryable();
        return await query.AnyAsync(x => x.Id!.Equals(id));
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = this.GetQueryable();
        return await query.ToArrayAsync();
    }

    public Task<TEntity> GetAsync(TKey id)
    {
        var query = this.GetQueryable();
        return query.FirstOrDefaultAsync(t => t.Id.Equals(id));
    }

    public virtual void UpdateAsync(TEntity entity)
    {
        GetDbSet().Update(entity);
    }
}
