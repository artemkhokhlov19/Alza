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
public class UpdateAsyncTests
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
        this.productService = new ProductService(
            productRepositoryMock.Object,
            mapperMock.Object,
            loggerMock.Object,
            createValidatorMock.Object
        );
    }

    [Test]
    public async Task UpdateDescriptionAsync_ValidData_ReturnsUpdatedResponse()
    {
        var productEditModel = new ProductEditModel { Description = "Updated Description" };
        var existingEntity = new ProductEntity
        {
            Id = 1,
            Name = "Test",
            Description = "Old Description",
            ImgUri = "TestUri",
            Price = 1000,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        };

        var updatedEntity = new ProductEntity
        {
            Id = 1,
            Name = "Test",
            Description = "Updated Description",
            ImgUri = "TestUri",
            Price = 1000,
            CreatedAt = existingEntity.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        var updatedResponse = new ProductResponse
        {
            Id = 1,
            Name = "Test",
            Description = "Updated Description",
            ImgUri = "TestUri",
            Price = 1000
        };

        productRepositoryMock.Setup(r => r.GetAsync(existingEntity.Id)).ReturnsAsync(existingEntity);
        mapperMock.Setup(m => m.Map(productEditModel, existingEntity)).Returns(updatedEntity);
        mapperMock.Setup(m => m.Map<ProductResponse>(updatedEntity)).Returns(updatedResponse);
        productRepositoryMock.Setup(u => u.UnitOfWork.Commit()).Verifiable();

        var result = await productService.UpdateDescriptionAsync(productEditModel, existingEntity.Id);

        productRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ProductEntity>(e => e == updatedEntity)), Times.Once);
        productRepositoryMock.Verify(u => u.UnitOfWork.Commit(), Times.Once);

        Assert.IsNotNull(result);
        Assert.That(result.Data.Description, Is.EqualTo(updatedResponse.Description));
        Assert.That(result.Data.Id, Is.EqualTo(updatedResponse.Id));
    }

    [Test]
    public async Task UpdateDescriptionAsync_NonExistingEntity_ReturnsEntityNotFound()
    {
        var productEditModel = new ProductEditModel { Description = "Updated Description" };
        productRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((ProductEntity)null);

        var result = await productService.UpdateDescriptionAsync(productEditModel, 1);

        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.Code, Is.EqualTo("EntityNotFound"));
    }
}
