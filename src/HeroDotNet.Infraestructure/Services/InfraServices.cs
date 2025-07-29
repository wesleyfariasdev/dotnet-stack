using HeroDotNet.Application.Services;
using HeroDotNet.Data.Repository;
using HeroDotNet.Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace HeroDotNet.Infraestructure.Services;

public static class InfraServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ProdutoRepository>();
        services.AddScoped<IProdutoRepository>(sp => new ProdutoRepositoryCacheDecorator
                                              (sp.GetRequiredService<ProdutoRepository>(),
                                               sp.GetRequiredService<IDatabase>()));
        return services;
    }
}
