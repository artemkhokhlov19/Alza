using Alza.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
