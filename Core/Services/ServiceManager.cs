using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ProductService> _productService;
        private readonly Lazy<BasketService> _basketService;

        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper, IBasketRepository basketRepository)
        {
            _productService = new Lazy<ProductService>(()=>new ProductService(unitOfWork,mapper));
            _basketService = new Lazy<BasketService>(()=>new BasketService(basketRepository, mapper));
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;
    }
}
