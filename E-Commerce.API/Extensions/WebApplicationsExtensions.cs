using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationsExtensions
    {
        public static async Task <WebApplication> SeedDbAsync(this WebApplication app)
        {

            // Create object from  type that implements IDbInitializer (Dependancy Injection)
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();




            return app;

        }
        public static WebApplication UseCustomExceptionMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }

    }
}
