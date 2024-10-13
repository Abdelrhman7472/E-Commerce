
global using Domain.Contracts;
global using Microsoft.EntityFrameworkCore;
global using Persistence;
global using Persistence.Data;
using E_Commerce.API.Extensions;
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
            #region Services

            builder.Services.AddCoreServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddPresentationServices();

      

            #endregion
            var app = builder.Build();

            #region Pipelines
            await app.SeedDbAsync();
            app.UseCustomExceptionMiddleWare();


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


            #endregion

           // async Task InitializeDbAsync(WebApplication app) // hattnfz awl haga fel request
    
        }
    }
}
