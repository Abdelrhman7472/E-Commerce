using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(d=>d.Price).HasColumnType("decimal(18,3)");

            //OwnsOne or OwnsMany (For 1 to 1 total participation)
            builder.OwnsOne(item => item.Product, product => product.WithOwner());

        }
    }
}
