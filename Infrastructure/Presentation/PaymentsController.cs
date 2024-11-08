using Services.Abstractions;
using Shared.BasketModels;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class PaymentsController(IServiceManager serviceManager  ) :ApiController
    {
        // lw 3andy input parameter w mab3tosh fe el template(segment) 3ady bas sa3tha haytb3t query string 

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result= await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);

            return Ok(result);
        }

        const string endpointSecret = "whsec_4bb985a3a0936a4eb0f22c8e08fe93fbac0f8fd445b3c07b4a3a98ed1ec8f244";


        [HttpPost("webHook")]
        public async Task<ActionResult> WebHook()
        {
            var json=await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            await serviceManager.PaymentService.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]!);

            return new EmptyResult();
            // new EmptyResult 3shan stripe msh metsani meni ay response

        }


    }
}
