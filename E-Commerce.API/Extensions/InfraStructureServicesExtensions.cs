using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Persistence.Repositories;
using StackExchange.Redis;
using Domain.Contracts;
using Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Shared.BasketModels;
using Domain.Entities.SecurityEntities;
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

            services.AddDbContext<StoreIdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentitySQLConnection"));
            });

            services.ConfigureIdentityService();
            services.ConfigureJwt(configuration);
            return services;
        }

        public static IServiceCollection ConfigureIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<User,IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<StoreIdentityContext>();// ba3mlha 3shan ba2y el types ely msh ma3ol leha register ma3 el AddIdnetity w mawgoda fe UserManager Ctor Zy ILogger , Ioptions and so on
            return services;
        }

        public static IServiceCollection ConfigureJwt(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtOptions =configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => // default leha haykon 3al cookie 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secretkey))

                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}
