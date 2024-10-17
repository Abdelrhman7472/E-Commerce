using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            // Beyakhod el instance w el object w el initalize 3amtn ely 3amlo fel app settings

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.on model creating bet3ml configuration ll type ely ana 3aml leha inhert bas hena msh 3ayzha 3shan ana 3amel my own types msh wakhed types gahza
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
