using AutoMapper;
using Domain.Entities.BasketEntities;
using Shared.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class BasketProfile :Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();

            CreateMap<BasketItem, BasketItemDTO>().ReverseMap(); // 3amltha 3shan msh hay3rf ye3ml map ben 2 many to many relation 
        }
    }
}
