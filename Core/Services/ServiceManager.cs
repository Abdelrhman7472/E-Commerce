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

        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _productService = new Lazy<ProductService>(()=>new ProductService(unitOfWork,mapper));
        }
        public IProductService ProductService => _productService.Value;
    }
}
