using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class Order : BaseEntity<Guid> //ay haga hatkon table fe DataBase hakhleha tewres mn BaseEntity 
    {
        public Order()
        {
            
        }
        public Order(string userEmail
            , Address shippingAddress
            , ICollection<OrderItem> orderItems,
            DeliveryMethod deliveryMethod,
            decimal subTotal
            
      )
        {
            Id = Guid.NewGuid();// Default value for guid is nafs 3add el arkam bas kolha 0 
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;


        }

        // User Email 
        public string UserEmail { get; set; }

        // Order Address

        public Address ShippingAddress { get; set; }//will store in one table in sql with order(1 to 1 Total Participation relation)
                                                    // (Zy fekret Complex Attributes in SQL)

        //Order Items 
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();//new list 3shan tkon initialized lma nenady el ctor bet3na badal ma tkon null  a3ml add fe null fa yermy exception
        // Collection Navigational Property

        // Payment Status
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;// default dataType for enum in sql is int

        // Delivery Method 
        public DeliveryMethod DeliveryMethod { get; set; } //Navigational Property

        public int? DeliveryMethodId { get; set; }

        //sub Total = quantity.Items * Price

        public decimal SubTotal { get; set; }

        // payment
        public string PaymentIntend { get; set; } = string.Empty;

        // Order Date
        //DateTimeOffset => bey7seb el wa2t bona2an 3ala el machine ely enta 3aleha 
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    }
}