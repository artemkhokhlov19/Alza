using Alza.Core.Models;
namespace Alza.Core.Data.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
{
    IUnitOfWork UnitOfWork { get; }

    Task<bool> ExistsAsync(TKey id);

    Task<TEntity> GetAsync(TKey id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task AddAsync(TEntity entity);

    void UpdateAsync(TEntity entity);
}
