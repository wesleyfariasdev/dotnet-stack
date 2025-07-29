using HeroDotNet.Data.HeroContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeroDotNet.Infraestructure.Db;

public static class DbServices
{
    public static IServiceCollection AddDbConfig(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["ConnectionStrings:DefaultConnection"];

        if (string.IsNullOrEmpty(connectionString))
            connectionString = config.GetConnectionString("DEV");

        services.AddDbContext<HeroContextDb>(q => q.UseSqlServer(connectionString,
                                                  q => q.MigrationsAssembly(typeof(HeroContextDb)
                                                        .Assembly.FullName)));

        return services;
    }
}
