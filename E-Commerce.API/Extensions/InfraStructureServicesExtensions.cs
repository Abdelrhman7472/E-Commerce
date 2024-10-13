using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Persistence.Repositories;
using StackExchange.Redis;
using Domain.Contracts;
namespace E_Commerce.API.Extensions
{
    public static class InfraStructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
   
         services.AddScoped<IDbInitializer, DbInitializer>();
         services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSQLConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>
                (_=>ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

         services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }

    }
}
