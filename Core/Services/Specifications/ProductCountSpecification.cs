using Domain.Entities.ProductEntites;
using Shared.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductCountSpecifications:Specifications<Product>
    {
        public ProductCountSpecifications(ProductSpecificationsParameters parameters)
            :base(product=>
        (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId)&&
        (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))



            { }

        
 

        
    }
}
