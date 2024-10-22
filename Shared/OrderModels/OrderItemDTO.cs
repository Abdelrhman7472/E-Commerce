using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderItemDTO
    {
        //public ProductInOrderItem Product { get; set; } // msh ha3mlha zy Domian 3shan el response yekon asr3 hakteb el properties beto3ha 


        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
