using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications: Specifications<Product>
    {
        //Use to retrieve product by Id
        public ProductWithBrandAndTypeSpecifications(int id ):base(product=>product.Id==id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            
        }  
        //Use to Get All Products
        public ProductWithBrandAndTypeSpecifications( string? sort,int?brandId,int?typeId):base(product=>
        (!brandId.HasValue||product.BrandId==brandId.Value)&&
        (!typeId.HasValue||product.TypeId == typeId.Value))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            
            if(!string.IsNullOrWhiteSpace(sort))
            {
                switch(sort.ToLower().Trim())
                {
                    case "pricedesc":
                        SetOrderByDescending(product => product.Price);
                        break;
                    case "priceasc":
                        SetOrderBy(product => product.Price);
                        break;
                    case "namedesc":
                        SetOrderByDescending(product => product.Name);
                        break;
                        default:
                        SetOrderBy(product => product.Price);
                        break;
                   
                }
            }
        }
    }
}
