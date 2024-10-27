using AutoMapper;
using Domain.Entities.SecurityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        // Lazy 3shan msh kol ma a3ml inject l ServiceManager ye3ml object(instance) mn kol service fe el constructor hata lw msh me7tagha fa Lazy 3shan el met7ded bas howa ely yet3mlo inject
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IPaymentService> _paymentService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<User>userManager,IOptions<JwtOptions> options,IConfiguration configuration)
        {
            _productService = new Lazy<IProductService>
                (() => new ProductService(unitOfWork, mapper));

            _basketService = new Lazy<IBasketService>
                (() => new BasketService(basketRepository, mapper));

            _authenticationService = new Lazy<IAuthenticationService>
                (() => new AuthenticationService(userManager,options,mapper));
            

            _orderService=new Lazy<IOrderService>(()=>new OrderService(mapper,unitOfWork,basketRepository));

            _paymentService = new Lazy<IPaymentService>(()=>new PaymentService(basketRepository,unitOfWork,mapper,configuration));
        }
        public Abstractions.IProductService ProductService => _productService.Value;

        public Abstractions.IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService =>_orderService.Value; // .Value=>> 3shan ye3ml execute ll lamda expresion w ye3nl execute l ely feha 

        public IPaymentService PaymentService =>_paymentService.Value;
    }
}
