using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductEntites;

namespace Persistence.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(product=>product.ProductBrand).WithMany().HasForeignKey(product=>product.BrandId);

            builder.HasOne(product=>product.ProductType).WithMany().HasForeignKey(product=>product.TypeId);

            builder.Property(p => p.Price).HasColumnType("decimal(18,3)");

        }
    }
}
