using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications : Specifications<Order>
    {
        public OrderWithIncludeSpecifications(Guid id) : base(order=>order.Id==id)
        {
            AddInclude(order => order.DeliveryMethod);    //Navigational Property
            AddInclude(order => order.OrderItems);        //Navigational Property
        }     
        public OrderWithIncludeSpecifications(string email) : base(order=>order.UserEmail==email)
        {
            AddInclude(order => order.DeliveryMethod); //Navigational Property
            AddInclude(order => order.OrderItems);     //Navigational Property

            SetOrderBy(o=>o.OrderDate); // 3shan lw taleb orders kter a3mlhom sorted bl date
        }
    }
}
