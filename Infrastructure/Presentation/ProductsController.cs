using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{

    [Authorize(Roles ="SuperAdmin")]
    public class ProductsController(IServiceManager serviceManager): ApiController
    {
        [HttpGet("AllProducts")]
        public async Task<ActionResult<PaginatedResult<ProductResultDTO>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(products);
            //Ok => bet7wl el type beta3y l JSON 
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDTO>>> GetAllBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);

        }   
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDTO>>> GetAllTypes()
        {
            var types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);

        }
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]             //Bet2olo eh el return fel response
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]  //Bet2olo eh el return fel response
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]//Bet2olo eh el return fel response
        [ProducesResponseType(typeof(ProductResultDTO), (int)HttpStatusCode.OK)]               //Bet2olo eh el return fel response
        [HttpGet("Product{id}")]

        public async Task<ActionResult<ProductResultDTO>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

    }
}
