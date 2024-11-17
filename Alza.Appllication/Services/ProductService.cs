using Alza.Appllication.Constants;
using Alza.Appllication.Utils;
using Alza.Contracts.DataObjects.Products;
using Alza.Core.BusinessResult;
using Alza.Core.Data.Extensions;
using Alza.Core.Models;
using Alza.Database.Data.Entities;
using Alza.Database.Data.Repositories;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Alza.Appllication.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    private readonly ILogger<ProductService> logger;
    private readonly IValidator<ProductCreateModel> createValidator;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<ProductService> logger,
        IValidator<ProductCreateModel> createValidator)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
        this.logger = logger;
        this.createValidator = createValidator;
    }

    /// <inheritdoc />
    public async Task<BusinessActionResult<ProductResponse>> CreateAsync(ProductCreateModel model)
    {
        var validationResult = createValidator.Validate(model);

        if (validationResult is not null && !validationResult.IsValid) 
        {
            logger.LogError(LogMessageTemplates.ValidationError, LoggerUtils.CombineErrorMessages(validationResult.Errors));
            return validationResult;
        }

        var productEntity = mapper.Map<ProductEntity>(model);
        productEntity.CreatedAt = DateTime.UtcNow;
        productEntity.UpdatedAt = DateTime.UtcNow;

        await productRepository.AddAsync(productEntity);
        productRepository.UnitOfWork.Commit();

        var response = mapper.Map<ProductResponse>(productEntity);

        return new BusinessActionResult<ProductResponse>(response, "Created", response.Id);
    }

    /// <inheritdoc />
    public async Task<BusinessActionResult<ProductResponse>> GetByIdAsync(int id)
    {
        var existingEntity = await productRepository.GetAsync(id);

        if (existingEntity is null)
        {
            logger.LogError(LogMessageTemplates.ItemNotFound, id);
            return BusinessResultHelper.EntityNotFound<ProductEntity>($"Product.Id: {id}");
        }

        return mapper.Map<ProductResponse>(existingEntity);
    }

    /// <inheritdoc />
    public async Task<BusinessActionResult<IEnumerable<ProductListItemResponse>>> GetListAsync()
    {
        var items = await productRepository.GetAllAsync();
        var itemsMapped = mapper.Map<IEnumerable<ProductListItemResponse>>(items); 
        return new BusinessActionResult<IEnumerable<ProductListItemResponse>>(itemsMapped);
    }

    /// <inheritdoc />
    public async Task<BusinessActionResult<PagedList<ProductListItemResponse>>> GetPagedAsync(PagedRequest request)
    {
        var totalItems = await productRepository.GetCountAsync();

        var items = await productRepository.GetPagedAsync(request.GetOffset(), request.GetLimit());

        var result = mapper.Map<IEnumerable<ProductListItemResponse>>(items ?? []);

        return result.ToPagedList(request.GetPage(), request.GetLimit(), totalItems);
    }

    /// <inheritdoc />
    public async Task<BusinessActionResult<ProductResponse>> UpdateDescriptionAsync(ProductEditModel model, int id)
    {
        var existingEntity = await productRepository.GetAsync(id);

        if (existingEntity is null)
        {
            logger.LogError(LogMessageTemplates.ItemNotFound, id);
            return BusinessResultHelper.EntityNotFound<ProductEntity>($"Product.Id: {id}");
        }

        existingEntity = mapper.Map(model, existingEntity);
        existingEntity.UpdatedAt = DateTime.UtcNow;

        productRepository.UpdateAsync(existingEntity);

        productRepository.UnitOfWork.Commit();

        return mapper.Map<ProductResponse>(existingEntity);
    }
}
