using Alza.Appllication.Mapping;
using Alza.Contracts.DataObjects.Products;
using Alza.Database.Data.Entities;
using AutoMapper;

namespace Alza.Tests.ProductServiceTests;

[TestFixture]
public class MapperTests
{
    private IMapper mapper;

    [SetUp]
    public void Setup()
    {
        mapper = AutoMapperFactory.CreateMapper();
    }

    [Test]
    public void ProductEntity_To_ProductListItemResponse_Mapping_IsValid()
    {
        var productEntity = new ProductEntity
        {
            Id = 1,
            Name = "Test Product",
            Price = 99.99M,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var result = mapper.Map<ProductListItemResponse>(productEntity);

        Assert.That(result.Id, Is.EqualTo(productEntity.Id));
        Assert.That(result.Name, Is.EqualTo(productEntity.Name));
        Assert.That(result.Price, Is.EqualTo(productEntity.Price));
    }

    [Test]
    public void ProductCreateModel_To_ProductEntity_Mapping_IsValid()
    {
        var createModel = new ProductCreateModel
        {
            Name = "New Product",
            Price = 49.99M
        };

        var result = mapper.Map<ProductEntity>(createModel);

        Assert.That(result.Name, Is.EqualTo(createModel.Name));
        Assert.That(result.Price, Is.EqualTo(createModel.Price));
        Assert.That(result.Id, Is.EqualTo(0)); 
    }
}
