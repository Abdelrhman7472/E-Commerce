using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
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

            services.ConfigureSwagger();
            return services;

        }
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options=>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter Bearer Token .",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
              
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                         Type=ReferenceType.SecurityScheme,
                         Id="Bearer"
                    }
                },
                new List<string>(){}
                    } });
            });
            return services; 

        }






    }
}
