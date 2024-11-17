using Alza.Appllication.Services;
using Alza.Contracts.DataObjects.Products;
using Alza.Core.Models;
using Alza.Database.Data.Entities;
using Alza.Database.Data.Repositories;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace Alza.Tests.ProductServiceTests;

[TestFixture]
public class GetAsyncTests
{
    private Mock<IProductRepository> productRepositoryMock;
    private Mock<IMapper> mapperMock;
    private Mock<ILogger<ProductService>> loggerMock;
    private Mock<IValidator<ProductCreateModel>> createValidatorMock;
    private ProductService productService;

    [SetUp]
    public void SetUp()
    {
        this.productRepositoryMock = new Mock<IProductRepository>();
        this.mapperMock = new Mock<IMapper>();
        this.loggerMock = new Mock<ILogger<ProductService>>();
        this.createValidatorMock = new Mock<IValidator<ProductCreateModel>>();

        this.productService = new ProductService(productRepositoryMock.Object, mapperMock.Object, loggerMock.Object, createValidatorMock.Object);
    }

    [Test]
    public async Task GetByIdAsync_ProductExists_ReturnsProductResponse()
    {
        var productEntity = new ProductEntity { Id = 1, Name = "Test Product" };
        productRepositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(productEntity);
        var productResponse = new ProductResponse { Id = 1, Name = "Test Product" };
        mapperMock.Setup(m => m.Map<ProductResponse>(productEntity)).Returns(productResponse);

        var result = await productService.GetByIdAsync(1);
        Assert.NotNull(result);
        Assert.That(productResponse.Id, Is.EqualTo(result.Data.Id));
        Assert.That(productResponse.Name, Is.EqualTo(result.Data.Name));
    }

    [Test]
    public async Task GetByIdAsync_NonExistingData_ReturnsNotFound()
    {
        productRepositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync((ProductEntity?)null);

        var result = await productService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.That(result.Code, Is.EqualTo("EntityNotFound"));
        Assert.That(result.Data, Is.Null);
    }

    [Test]
    public async Task GetAllAsync_ExistingData_ReturnsListOfData()
    {
        var mockEntities = Enumerable.Range(1, 100).Select(i => new ProductEntity
        {
            Id = i,
            Name = $"Product {i}",
            Price = i * 10,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        }).ToArray();

        var mockEntitiesMapped = Enumerable.Range(1, 100).Select(i => new ProductListItemResponse
        {
            Id = i,
            Name = $"Product {i}",
            Price = i * 10,
        }).ToArray();

        productRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mockEntities);
        mapperMock.Setup(m => m.Map<IEnumerable<ProductListItemResponse>>(mockEntities)).Returns(mockEntitiesMapped);
        var result = await productService.GetListAsync();

        Assert.NotNull(result);
        Assert.IsNotEmpty(result.Data);
        Assert.That(result.Code, Is.EqualTo("Success"));
    }

    [Test]
    public async Task GetPagedAsync_ExistingData_ReturnsPagedListOfData()
    {
        var mockEntities = Enumerable.Range(1, 100).Select(i => new ProductEntity
        {
            Id = i,
            Name = $"Product {i}",
            Price = i * 10,
        }).ToArray();

        var mockEntitiesMapped = Enumerable.Range(1, 100).Select(i => new ProductListItemResponse
        {
            Id = i,
            Name = $"Product {i}",
            Price = i * 10,
        }).ToArray();

        var limit = 10;
        var exceedingOffset = 110;
        var normalOffset = 15;

        productRepositoryMock.Setup(r => r.GetCountAsync()).ReturnsAsync(mockEntities.Count);

        productRepositoryMock.Setup(r => r.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((int offset, int limit) => mockEntities.Skip(offset).Take(limit).ToList());

        mapperMock.Setup(m => m.Map<IEnumerable<ProductListItemResponse>>(mockEntities)).Returns(mockEntitiesMapped);

        var normalPagedRequest = new PagedRequest() { Limit = limit, Offset = normalOffset };
        var exceedingPagedRequest = new PagedRequest() { Limit = limit, Offset = exceedingOffset };

        var resultNormal = await productService.GetPagedAsync(normalPagedRequest);
        var resultExceeding = await productService.GetPagedAsync(exceedingPagedRequest);

        Assert.That(resultNormal.Data.CurrentPage, Is.EqualTo(2));
        Assert.That(resultExceeding.Data.CurrentPage, Is.EqualTo(12));
        Assert.That(resultNormal.Data.TotalCount, Is.EqualTo(100));
        Assert.That(resultExceeding.Data.TotalCount, Is.EqualTo(100));
    }
}
