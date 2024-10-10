using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    // lw hatet ba3d "api/[controller]" 3amlt /Action kda esm el action aw el endpoint hayzhar fel Route
    public class ProductsController(IServiceManager serviceManager):ControllerBase
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
        [HttpGet("Product{id}")]
        public async Task<ActionResult<ProductResultDTO>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

    }
}
