using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record DeliveryMethodResultDTO  
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; } // hatkon zy => With In 3 days , with in 1 Day ....s
        public decimal Cost { get; set; }
    }
}
