using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;

        public DbInitializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task InitializeAsync()
        {
            try
            {

                // Create any DataBase if it doesn't exist and apply migrations
                if (_storeContext.Database.GetPendingMigrations().Any())
                    await _storeContext.Database.MigrateAsync();// create pending DB
                                                                // GetPendingMigrations: hayshof migration mat3mlsh leha apply abl kda(msh mawgoda fe EF migaration history

                //apply Data seeding
                if (!_storeContext.ProductTypes.Any())
                {
                    // 1: Read types from file as string 
                    var typesData = await File.ReadAllTextAsync(@"..\Core\Persistence\Data\Seeding\types.json");
                    // 2: transform into c# objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // 3: Add to DB and savechanges
                    if (types is not null && types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(types);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                if (!_storeContext.ProductBrands.Any())
                {
                    // 1: Read types from file as string 
                    var brandsData = await File.ReadAllTextAsync(@"..\Core\Persistence\Data\Seeding\brands.json");
                    // 2: transform into c# objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // 3: Add to DB and savechanges
                    if (brands is not null && brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                if (!_storeContext.Products.Any())
                {
                    // 1: Read types from file as string 
                    var productsData = await File.ReadAllTextAsync(@"..\Core\Persistence\Data\Seeding\products.json");
                    // 2: transform into c# objects
                    var produts = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // 3: Add to DB and savechanges
                    if (produts is not null && produts.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(produts);
                        await _storeContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ) 
            {
                throw;
            }
        }
    }
}
