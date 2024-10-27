global using Stripe;
global using Product = Domain.Entities.ProductEntites.Product;
using AutoMapper;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Shared.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class PaymentService(IBasketRepository basketRepository ,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IConfiguration configuration ) : IPaymentService
    {
        public async Task<CustomerBasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("StripeSettings")["SecretKey"];
            // Get Basket =>>SubTotal =>> Product
            // Repository =>> Create Object from type you select
            var basket = await basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);

            foreach (var item in basket.Items)
            {
                var product= await unitOfWork.GetRepository<Product,int>()
                    .GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price= product.Price;

            }

            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method is selected ");

            var method = await unitOfWork.GetRepository<DeliveryMethod, int>()
            .GetAsync(basket.DeliveryMethodId.Value) 
           ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = method.Price;

            //long 3shan stripe beyakhod long bas 
            var amount = (long)(basket.Items.Sum(item => item.Quantity * item.Price) + basket.ShippingPrice) * 100;

            var service=new PaymentIntentService();

            if(string.IsNullOrWhiteSpace(basket.PaymentIntendId))
            {
                //create
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent=await service.CreateAsync(createOptions);

                basket.PaymentIntendId=paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;


            }
            else
            {
                //update

                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await service.UpdateAsync(basket.PaymentIntendId, updateOptions);

            }
            await basketRepository.UpdateBasketAsync(basket);

            return mapper.Map<CustomerBasketDTO>(basket);

        }
    }
}
