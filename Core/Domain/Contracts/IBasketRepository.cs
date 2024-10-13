﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string Id);
        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket,TimeSpan? timeToLive=null);
        public Task<bool> DeleteBasketAsync(string Id);
    }
}