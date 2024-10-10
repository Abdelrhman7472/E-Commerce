using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        //Use to retrieve product by Id
        public ProductWithBrandAndTypeSpecifications(int id) : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);

        }
        //Use to Get All Products
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationsParameters parameters)
            : base(product =>
        (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId) &&
        (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.Search) ||product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);

            ApplyPagination(parameters.PageIndex, parameters.PageSize);


            if(parameters.Sort is not null)
            {
                switch(parameters.Sort)
                {
                    case ProductSortingOptions.pricedesc:
                        SetOrderByDescending(product => product.Price);
                        break;
                    case ProductSortingOptions.priceasc:
                        SetOrderBy(product => product.Price);
                        break;
                    case ProductSortingOptions.namedesc:
                        SetOrderByDescending(product => product.Name);
                        break;
                    case ProductSortingOptions.nameasc:
                        SetOrderBy(product => product.Name);
                        break;

                    default:
                        break;
                   
                }
            }
        }
    }
}
