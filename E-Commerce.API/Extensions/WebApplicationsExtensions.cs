using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationsExtensions
    {
        public static async Task <WebApplication> SeedDbAsync(this WebApplication app)
        {

            // Create object from  type that implements IDbInitializer (Dependancy Injection)
            // Call it when the app starts before request (3shan a3ml el data seeding awl haga fel request 3andy)
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();




            return app;

        }
        public static WebApplication UseCustomExceptionMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }

    }
}
