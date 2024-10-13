using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository,IMapper mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
    =>    await basketRepository.DeleteBasketAsync(id);

        public async Task<CustomerBasketDTO?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);

            return basket is null ?  throw new BasketNotFoundException(id) : 
                mapper.Map<CustomerBasketDTO>(basket);
        }


        public async Task<CustomerBasketDTO?> UpdateBasketAsync(CustomerBasketDTO basket)
        {
            var customerBasket=mapper.Map<CustomerBasket>(basket);

            var updatedBasket = await basketRepository.UpdateBasketAsync(customerBasket);

            return updatedBasket is null ? throw new Exception("Can't Update Basket Now!!!"):
                mapper.Map<CustomerBasketDTO>(updatedBasket);
        }

 
    }
}
