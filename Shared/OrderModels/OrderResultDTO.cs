using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderResultDTO
    {
        public Guid Id { get; set; }
        // User Email 

        public string UserEmail { get; init; }

        // Order Address

        public AddressDTO ShippingAddress { get; init; }//will store in one table in sql with order(1 to 1 Total Participation relation)
                                                        // (Zy fekret Complex Attributes in SQL)

        //Order Items 
        public ICollection<OrderItemDTO> OrderItems { get; init; } = new List<OrderItemDTO>(); //new list 3shan tkon initialized lma nenady el ctor bet3na badal ma tkon null a3ml add fe null fa yermy exception 
        // Collection Navigational Property

        // Payment Status
        public string Status { get; init; } // kda kda fl Service haykon 3andy access aghyar el enum l ely ana 3ayzo mn string 

        // Delivery Method 
        public string DeliveryMethod { get; init; }// met7ag fel result bas a3rf esm el DeliveryMethod


        //sub Total = quantity.Items * Price

        public decimal SubTotal { get; init; }

        // payment
        public string PaymentIntend { get; init; } = string.Empty;

        // Order Date
        //DateTimeOffset => bey7seb el wa2t bona2an 3ala el machine ely enta 3aleha 
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.Now;

        public decimal Total { get; init; }
    }
}
