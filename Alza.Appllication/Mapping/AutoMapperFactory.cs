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
    }
}
