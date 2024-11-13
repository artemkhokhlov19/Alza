using Alza.Appllication.Mapping;
using Alza.Appllication.Services;
using Alza.Database.Context;
using Alza.Database.Data.Repositories;
using Alza.Host.Extensions.OpenApi;
using Asp.Versioning;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Alza.Host.Extensions;

public static class SeviceExtensions
{
    public static void AddOpenApiSpecification(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var apiVersioningBuilder = serviceCollection.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.ReportApiVersions = true;
            o.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        });

        apiVersioningBuilder.AddApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Title = "API V2", Version = "v2" });
            options.OperationFilter<SwaggerDefaultValues>();
        });
    }

    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }

    public static IServiceCollection AddScopedRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        IMapper mapper = AutoMapperFactory.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionString");
        services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IProductDbContext, ProductDbContext>();

        return services;
    }

    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
        if (scopeFactory != null)
        {
            using var scope = scopeFactory.CreateScope();
            scope.ServiceProvider.GetRequiredService<ProductDbContext>().Migrate();
        }
    }
}
