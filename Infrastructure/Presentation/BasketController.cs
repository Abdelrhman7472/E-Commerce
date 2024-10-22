using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{

    public class BasketController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDTO>> Get(string id)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> Update(CustomerBasketDTO basketDTO)
        {
            var basket= await serviceManager.BasketService.UpdateBasketAsync(basketDTO);
            return Ok(basket);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerBasketDTO>> Delete(string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }


    }
}
