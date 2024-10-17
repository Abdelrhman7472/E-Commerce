global using Services.Abstractions;
global using Domain.Contracts;
global using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Services.Specifications;
using Domain.Exceptions;

namespace Services
{
    // Ka2nha Controller fe el MVC
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : Abstractions.IProductService
    {
    

        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            // 1 : Retrieve All Brands => UnitOfWork (3shan unitOfWork Heya ely betklm el context)
            var brands=await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();

            // 2 : Map to Brand => IMapper: AutoMapper
            var brandsResult=_mapper.Map<IEnumerable<BrandResultDTO>>(brands);
            // 3 : Return Result
            return brandsResult;
            
        }

        public async Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationsParameters parameters )
        {  
            // 1 : Retrieve All Products => UnitOfWork (3shan unitOfWork Heya ely betklm el context)
            var products = await _unitOfWork.GetRepository<Product, int>()
                .GetAllAsync( new ProductWithBrandAndTypeSpecifications(parameters));

            // 2 : Map to product => IMapper: AutoMapper
            var productsResult = _mapper.Map<IEnumerable<ProductResultDTO>>(products);

             var count= productsResult.Count();

             var totalCount= await _unitOfWork.GetRepository<Product, int>()
                .CountAysnc(new ProductCountSpecifications(parameters)); ;
            var result=new PaginatedResult<ProductResultDTO> (
                parameters.PageIndex
                , count,
                totalCount
                 ,
                 productsResult);
            return result; 
            
        }

        public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
        {
            // 1 : Retrieve All Types => UnitOfWork (3shan unitOfWork Heya ely betklm el context)
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            // 2 : Map to Types => IMapper: AutoMapper
            var typesResult = _mapper.Map<IEnumerable<TypeResultDTO>>(types);
            // 3 : Return Result
            return typesResult;
        }

        public async Task<ProductResultDTO?> GetProductByIdAsync(int id)
        {

            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(new ProductWithBrandAndTypeSpecifications(id));

            return product is null ? throw new ProductNotFoundException(id):_mapper.Map<ProductResultDTO>(product);
            
        }
    }
}
