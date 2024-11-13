using Alza.Contracts.DataObjects.Products;
using Alza.Core.Data.Extensions;
using Alza.Core.Models;
using Alza.Database.Data.Entities;
using Alza.Database.Data.Repositories;
using AutoMapper;

namespace Alza.Appllication.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    //TODO: logs

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }
        

    public async Task<ProductResponse> CreateAsync(ProductCreateModel model)
    {
        var productEntity = mapper.Map<ProductEntity>(model);
        productEntity.CreatedAt = DateTime.UtcNow;
        productEntity.UpdatedAt = DateTime.UtcNow;

        await productRepository.AddAsync(productEntity);
        productRepository.UnitOfWork.Commit();

        return mapper.Map<ProductResponse>(productEntity);
    }

    public async Task<ProductResponse> GetByIdAsync(int id)
    {
        var existingEntity = await productRepository.GetAsync(id);

        if (existingEntity is null)
        {
            //TODO: redo to businesactionresults
            throw new FileNotFoundException();
        }

        return mapper.Map<ProductResponse>(existingEntity);
    }

    public async Task<IEnumerable<ProductListItemResponse>> GetListAsync()
    {
        var items = await productRepository.GetAllAsync();
        return mapper.Map<IEnumerable<ProductListItemResponse>>(items);
    }

    public async Task<PagedList<ProductListItemResponse>> GetPagedAsync(PagedRequest request)
    {
        var totalItems = await productRepository.GetCountAsync();

        var items = await productRepository.GetPagedAsync(request.GetOffset(), request.GetLimit());

        var result = mapper.Map<IEnumerable<ProductListItemResponse>>(items ?? []);

        return result.ToPagedList(request.GetPage(), request.GetLimit(), totalItems);
    }

    public async Task<ProductResponse> UpdateAsync(ProductEditModel model, int id)
    {
        var existingEntity = await productRepository.GetAsync(id);

        if (existingEntity is null)
        {
            //TODO: redo to businesactionresults
            throw new FileNotFoundException();
        }

        existingEntity = mapper.Map(model, existingEntity);

        productRepository.UpdateAsync(existingEntity);

        productRepository.UnitOfWork.Commit();

        return mapper.Map<ProductResponse>(existingEntity);
    }

    
}
