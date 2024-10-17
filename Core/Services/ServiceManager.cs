using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<User>userManager,IOptions<JwtOptions> options)
        {
            _productService = new Lazy<IProductService>
                (() => new ProductService(unitOfWork, mapper));

            _basketService = new Lazy<IBasketService>
                (() => new BasketService(basketRepository, mapper));

            _authenticationService = new Lazy<IAuthenticationService>
                (() => new AuthenticationService(userManager,options));
        }
        public Abstractions.IProductService ProductService => _productService.Value;

        public Abstractions.IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
