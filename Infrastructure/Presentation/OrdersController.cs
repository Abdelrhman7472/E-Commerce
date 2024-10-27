using Microsoft.AspNetCore.Authorization;
using Services.Abstractions;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) :ApiController
    {
        [HttpPost]

        public async Task<ActionResult<OrderResultDTO>> CreateOrder(OrderRequest request)
        {
           // User ely taht de a2dar ageb beha values mn el Token beta3y (ay claims ely fel token gowa User Property de

            var email = User.FindFirstValue(ClaimTypes.Email);
            var order= await serviceManager.OrderService.CreateOrUpdateOrderAsync(request, email);
            //return Created();// msh ba2dar ab3t el value nafsha feha hatrg3 el uri bta3 el OrderResultDTO msh el OrderResultDTO nafso

             return Ok(order);  
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderResultDTO>>> GetOrders()
        {
            // User ely taht de a2dar ageb beha values mn el Token beta3y (ay claims ely fel token gowa User Property de

            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await serviceManager.OrderService.GetOrdersByEmailAsync(email);
            //return Created();// msh ba2dar ab3t el value nafsha feha hatrg3 el uri bta3 el OrderResultDTO msh el OrderResultDTO nafso

            return Ok(orders);

        }
        [HttpGet("GetOrderById")]
        public async Task<ActionResult<OrderResultDTO>> GetOrderById(Guid id)
        {

            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            //return Created();// msh ba2dar ab3t el value nafsha feha hatrg3 el uri bta3 el OrderResultDTO msh el OrderResultDTO nafso

            return Ok(order);

        }  
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<OrderResultDTO>> DeliveryMethods()
        {

            var methods = await serviceManager.OrderService.GetDeliveryMethodAsync();
            //return Created();// msh ba2dar ab3t el value nafsha feha hatrg3 el uri bta3 el OrderResultDTO msh el OrderResultDTO nafso

            return Ok(methods);

        }

    }
}
