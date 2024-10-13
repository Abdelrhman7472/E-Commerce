using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace E_Commerce.API.Extensions
{
    public static class PresentationServicesExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        { 
             services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
          
             services.Configure<ApiBehaviorOptions> // model state
                    (options =>
                    {
                        options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;       // to chnage  default model state
                    }
          
                    );
          
             // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
             services.AddEndpointsApiExplorer();
             services.AddSwaggerGen();
          

            return services;

        }

   

        
    }
}
