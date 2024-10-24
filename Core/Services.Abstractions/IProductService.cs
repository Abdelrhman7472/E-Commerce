﻿global using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.ProductModels;

namespace Services.Abstractions
{
    public interface IProductService
    {
        public Task <PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationsParameters parameters);
        public Task<ProductResultDTO> GetProductByIdAsync(int id );
         
        public Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();


    }
}
