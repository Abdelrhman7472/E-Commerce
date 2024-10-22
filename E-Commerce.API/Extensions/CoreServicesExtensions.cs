using Services.Abstractions;
using Services;
using Shared.BasketModels;

namespace E_Commerce.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
         
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);// reference kol el file ely hatstkhdm feh el mapping aw ay assembly me7tago fe scope mo3yn

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;

        }
    }
}
