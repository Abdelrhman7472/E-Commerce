using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBasketService
    {
       public Task<CustomerBasketDTO?> GetBasketAsync(string id);
       public Task<CustomerBasketDTO?> UpdateBasketAsync(CustomerBasketDTO customerBasket);
       public Task<bool> DeleteBasketAsync(string id);


    }
}
