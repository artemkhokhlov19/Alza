﻿using Alza.Core.Data.Repositories;
using Alza.Database.Data.Entities;

namespace Alza.Database.Data.Repositories;

public interface IProductRepository : IRepository<ProductEntity, int>
{
    Task<int> GetCountAsync();

    Task<IEnumerable<ProductEntity>> GetPagedAsync(int offset, int limit);
}
