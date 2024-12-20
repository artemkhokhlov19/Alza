﻿using Alza.Appllication.Mapping;
using Alza.Appllication.Services;
using Alza.Contracts.DataObjects.Products;
using Alza.Database.Context;
using Alza.Database.Data.Repositories;
using Alza.Host.Extensions.OpenApi;
using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
            options.OperationFilter<SwaggerDefaultValues>();
        });
        serviceCollection.ConfigureOptions<SwaggerVersionsConfigurator>();
    }

    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }

    public static IServiceCollection AddScopedRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var useMockData = configuration.GetValue<bool>("UseMock");

        if (useMockData)
        {
            services.AddScoped<IProductRepository, MockProductRepository>();
        }
        else
        {
            services.AddScoped<IProductRepository, ProductRepository>(); 
        }

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

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ProductCreateModel>();
        return services;
    }
}
