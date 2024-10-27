using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Shared.OrderModels
{
    public record OrderRequest // Your Request to Create Order
    {
        public string basketId { get; set; }             // To create order front-end must send this Data
        public AddressDTO shipToAddress { get; set; }  // To create order front-end must send this Data
        public int DeliveryMethodId { get; set; }        // To create order front-end must send this Data 
    }
}
