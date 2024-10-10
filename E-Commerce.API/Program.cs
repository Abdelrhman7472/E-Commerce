
global using Domain.Contracts;
global using Microsoft.EntityFrameworkCore;
global using Persistence;
global using Persistence.Data;
using E_Commerce.API.Factories;
using E_Commerce.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Persistence.Repositories;
using Services;
using Services.Abstractions;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            builder.Services.AddScoped<IDbInitializer,DbInitializer>();

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            builder.Services.AddScoped<IServiceManager,ServiceManager>();
       
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);// reference kol el file ely hatstkhdm feh el mapping aw ay assembly me7tago fe scope mo3yn
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            });


            builder.Services.Configure<ApiBehaviorOptions> // model state
                (options =>
                {
                    options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;       // to chnage default model state
                }

                );





            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
           await InitializeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();             
            
            //app.UseStaticFiles(new StaticFileOptions                        Lw el mkan ely feh el static file msh wwwroot(default) 
            //{                                                               Lw el mkan ely feh el static file msh wwwroot(default) 
            //    FileProvider = new PhysicalFileProvider("File Path here")   Lw el mkan ely feh el static file msh wwwroot(default) 
            //});                                                             Lw el mkan ely feh el static file msh wwwroot(default) 

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();


            async Task InitializeDbAsync(WebApplication app) // hattnfz awl haga fel request
            {
                // Create object from  type that implements IDbInitializer (Dependancy Injection)
                using var scope =app.Services.CreateScope();
                var dbInitializer =scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeAsync();
                
            }
        }
    }
}
