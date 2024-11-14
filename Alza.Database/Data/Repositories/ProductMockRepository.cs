using Alza.Database.Context;
using Alza.Database.Data.Entities;
using Alza.Database.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Alza.Database.Data.Repositories;

public class MockProductRepository : BaseRepository<ProductEntity, int>, IProductRepository
{
    private readonly List<ProductEntity> _mockProducts;

    public MockProductRepository(IProductDbContext unitOfWork) : base(unitOfWork)
    {
        _mockProducts = GenerateMockProducts(20);
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(_mockProducts.Count);
    }

    public Task<IEnumerable<ProductEntity>> GetPagedAsync(int offset, int limit)
    {
        var pagedProducts = _mockProducts.Skip(offset).Take(limit).ToArray();
        return Task.FromResult(pagedProducts.AsEnumerable());
    }

    public new Task AddAsync(ProductEntity product)
    {
        _mockProducts.Add(product);
        return Task.CompletedTask;
    }

    public new Task UpdateAsync(ProductEntity product)
    {
        var existingProduct = _mockProducts.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
        }
        return Task.CompletedTask;
    }

    public Task<ProductEntity> GetByIdAsync(int id)
    {
        var product = _mockProducts.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    protected override DbSet<ProductEntity> GetDbSet()
    {
        var mockSet = new Mock<DbSet<ProductEntity>>();
        mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.Provider).Returns(_mockProducts.AsQueryable().Provider);
        mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.Expression).Returns(_mockProducts.AsQueryable().Expression);
        mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.ElementType).Returns(_mockProducts.AsQueryable().ElementType);
        mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.GetEnumerator()).Returns(_mockProducts.AsQueryable().GetEnumerator());
        return mockSet.Object;
    }

    public new Task<bool> ExistsAsync(int id)
    {
        var exists = _mockProducts.Any(p => p.Id == id);
        return Task.FromResult(exists);
    }

    public new Task<ProductEntity> GetAsync(int id)
    {
        var product = _mockProducts.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public new Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        return Task.FromResult(_mockProducts.AsEnumerable());
    }

    private List<ProductEntity> GenerateMockProducts(int count)
    {
        var products = new List<ProductEntity>();
        for (int i = 1; i <= count; i++)
        {
            products.Add(new ProductEntity
            {
                Id = i,
                Name = $"Mock Product {i}",
                Price = 10.99m + i,
                Description = $"Description for Mock Product {i}"
            });
        }
        return products;
    }
}
