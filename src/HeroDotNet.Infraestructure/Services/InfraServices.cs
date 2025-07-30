using HeroDotNet.Application.Commands.ProdutoCommand.Handlers;
using HeroDotNet.Application.Services;
using HeroDotNet.Data.Repository;
using HeroDotNet.Domain.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace HeroDotNet.Infraestructure.Services;

public static class InfraServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        services.AddScoped<ProdutoRepository>();

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("Redis")
                ?? throw new InvalidOperationException("Redis connection string not found.");
            return ConnectionMultiplexer.Connect(connectionString);
        });

        services.AddScoped<IProdutoRepository>(sp =>
        {
            var innerRepo = sp.GetRequiredService<ProdutoRepository>();
            var redis = sp.GetRequiredService<IConnectionMultiplexer>();
            var db = redis.GetDatabase();
            return new ProdutoRepositoryCacheDecorator(innerRepo, db);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProdutoHandler).Assembly));

        return services;
    }
}