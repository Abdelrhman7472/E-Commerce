using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    class OrderWithPaymentIntentIdSpecifications : Specifications<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId) : base(order=>order.PaymentIntend==paymentIntentId)
        {

        }
    }
}
