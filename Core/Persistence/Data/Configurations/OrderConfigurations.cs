using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderEntity= Domain.Entities.OrderEntities.Order;
namespace Persistence.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, address => address.WithOwner());

            builder.HasMany(order => order.OrderItems).WithOne();// withone bas mn gher => 3shan ana msh 3amel hnak navigational property w heya by default betkon one lw msh 3amel navigational property

            builder.Property(order=>order.PaymentStatus).HasConversion(s=>s.ToString(), //Convert enum to string in Sql
                s=>Enum.Parse<OrderPaymentStatus>(s));                                  //Convert enum to string in Sql


            builder.HasOne(order => order.DeliveryMethod).WithMany()
                .OnDelete(DeleteBehavior.SetNull); // Set Null 3shan lma a3ml delete l order bl delivery method de may3mlsh l kol el orders bnafs el delivery method de delete (Da kda lw kanet Cascade msh SetNull)

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,3)");
        
        }
    }
}
