using Alza.Appllication.Services;
using Alza.Contracts.DataObjects.Products;
using Alza.Database.Data.Entities;
using Alza.Database.Data.Repositories;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace Alza.Tests.ProductServiceTests;

[TestFixture]
public class CreateAsyncTests
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
        this.productService = new ProductService(
            productRepositoryMock.Object, 
            mapperMock.Object, 
            loggerMock.Object, 
            createValidatorMock.Object
        );
    }

    [Test]
    public async Task CreateAsync_ValidData_ReturnsMappedProductResponse()
    {
        var productCreateModel = new ProductCreateModel
        {
            Name = "Test",
            Description = "Test Description",
            ImgUri = "TestUri",
            Price = 1000
        };

        var productEntityMapped = new ProductEntity
        {
            Name = "Test",
            Description = "Test Description",
            ImgUri = "TestUri",
            Price = 1000,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var productResponseMapped = new ProductResponse
        {
            Name = "Test",
            Description = "Test Description",
            ImgUri = "TestUri",
            Price = 1000
        };

        mapperMock.Setup(m => m.Map<ProductEntity>(It.IsAny<ProductCreateModel>())).Returns(productEntityMapped);
        mapperMock.Setup(m => m.Map<ProductResponse>(It.IsAny<ProductEntity>())).Returns(productResponseMapped);
        productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<ProductEntity>())).Returns(Task.CompletedTask);

        productRepositoryMock.Setup(u => u.UnitOfWork.Commit()).Verifiable();


        var result = await productService.CreateAsync(productCreateModel);

        productRepositoryMock.Verify(r => r.AddAsync(It.Is<ProductEntity>(e => e == productEntityMapped)), Times.Once);

        Assert.IsNotNull(result);
        Assert.That(result.Data.Name, Is.EqualTo(productResponseMapped.Name));
        Assert.That(result.Data.Description, Is.EqualTo(productResponseMapped.Description));
        Assert.That(result.Data.ImgUri, Is.EqualTo(productResponseMapped.ImgUri));
        Assert.That(result.Data.Price, Is.EqualTo(productResponseMapped.Price));
    }

    //TODO
    [Test]
    public async Task CreateAsync_InvalidData_MissingName_ReturnsError()
    {
        var productCreateModel = new ProductCreateModel
        {
            Description = "Test Description",
            ImgUri = "TestUri",
            Price = 1000
        };

        var productResponseMapped = new ProductResponse
        {
            Name = "Test", 
            Description = "Test Description",
            ImgUri = "TestUri",
            Price = 1000
        };

        mapperMock.Setup(m => m.Map<ProductEntity>(It.IsAny<ProductCreateModel>())).Returns((ProductEntity)null);
        productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<ProductEntity>())).Returns(Task.CompletedTask);

        var errorResult = new BusinessActionResult<ProductResponse>("Validation error", "Name is required.");

        var result = await productService.CreateAsync(productCreateModel);

        productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ProductEntity>()), Times.Never);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Parameters, Is.EqualTo("Name is required."));
    }
}
