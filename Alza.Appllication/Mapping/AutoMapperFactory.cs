using Alza.Contracts.DataObjects.Products;
using Alza.Database.Data.Entities;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace Alza.Appllication.Mapping;

public static class AutoMapperFactory
{
    public static IMapper CreateMapper()
    {
        var config = CreateConfig();
        return config.CreateMapper();
    }

    private static MapperConfiguration CreateConfig()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddCollectionMappers();

            MapProducts(cfg);
        });
    }

    private static void MapProducts(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<ProductEntity, ProductListItemResponse>();
        cfg.CreateMap<ProductEntity, ProductResponse>();
        cfg.CreateMap<ProductCreateModel, ProductEntity>();
        cfg.CreateMap<ProductEditModel, ProductEntity>();
    }
}
