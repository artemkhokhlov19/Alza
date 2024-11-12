using Alza.Host.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var allowLocalhostOrigin = "_allowLocalgost";

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddOpenApiSpecification(configuration);

services.AddCors(options =>
{
    options.AddPolicy(
        name: allowLocalhostOrigin,
        policy =>
        {
            policy.WithOrigins("https://localhost");
        });
});

services.AddSwaggerGen(options =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

services
    .AddScopedServices()
    .AddScopedRepositories()
    .AddAutoMapper()
    .AddDatabase(configuration);

var app = builder.Build();

app.MigrateDatabase();
app.AddOpenApi();
app.UseHttpsRedirection();
app.MapControllers(); 

if (app.Environment.IsDevelopment())
{
    app.UseCors();
}

app.Run();