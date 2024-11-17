using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Alza.Host.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder AddOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                var baseUrl = $"https://{httpReq.Host.Value}";
                swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{baseUrl}" } };
            });
        });

        app.UseSwaggerUI(options =>
        {
            var descriptions = ((WebApplication)app).DescribeApiVersions();
            var groupNames = descriptions.Select(x => x.GroupName);

            foreach(var description in groupNames)
            {
                var url = $"/swagger/{description}/swagger.json";
                var name = description.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });
        return app;
    }
}
