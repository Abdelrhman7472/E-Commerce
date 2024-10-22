using AutoMapper;
using Domain.Entities.SecurityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _orderService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<User>userManager,IOptions<JwtOptions> options)
        {
            _productService = new Lazy<IProductService>
                (() => new ProductService(unitOfWork, mapper));

            _basketService = new Lazy<IBasketService>
                (() => new BasketService(basketRepository, mapper));

            _authenticationService = new Lazy<IAuthenticationService>
                (() => new AuthenticationService(userManager,options));


            _orderService=new Lazy<IOrderService>(()=>new OrderService(mapper,unitOfWork,basketRepository));
        }
        public Abstractions.IProductService ProductService => _productService.Value;

        public Abstractions.IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService =>_orderService.Value; // .Value=>> 3shan ye3ml execute ll lamda expresion w ye3nl execute l ely feha 
    }
}
