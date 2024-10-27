using OrderAddress = Domain.Entities.OrderEntities.Address;
using AutoMapper;
using Domain.Entities.BasketEntities;
using Domain.Entities.OrderEntities;
using Domain.Entities.ProductEntites;
using Domain.Exceptions;
using Services.Specifications;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IMapper mapper
        , IUnitOfWork unitOfWork,
        IBasketRepository basketRepository) : IOrderService

    {    
        private OrderItem CreateOrderItem(BasketItem item, Product product)
        
          =>  new OrderItem(new ProductInOrderItem(product.Id,product.Name,product.PictureUrl),item.Quantity,product.Price);

        public async Task<OrderResultDTO> CreateOrUpdateOrderAsync(OrderRequest request, string userEmail)
        {
            // Address
            var address=mapper.Map<OrderAddress>(request.shipToAddress);
            // order Items => Basket=>BasketItems=>OrderItems
            var basket= await basketRepository.GetBasketAsync(request.basketId)
                ??throw new BasketNotFoundException(request.basketId);
            
            var orderItems=new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var product=await unitOfWork.GetRepository<Product,int>().GetAsync(item.Id)
                    ??throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item,product));

            }


            // Delivery
            var deliveryMethod= await unitOfWork.GetRepository<DeliveryMethod,int>().GetAsync(request.DeliveryMethodId)
                ??throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);


            var orderRepo = unitOfWork.GetRepository<Order, Guid>();

            var existingOrder = await orderRepo.
                GetAsync(new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntendId));
            if(existingOrder is not null)
              orderRepo.Delete(existingOrder);
            

            // subtotal
            var subTotal=orderItems.Sum(item=>item.Price* item.Quantity);

            // save to DB
            var order = new Order(userEmail, address,orderItems,deliveryMethod , subTotal ,basket.PaymentIntendId);

            await orderRepo.AddAsync(order);


            await unitOfWork.SaveChangesAsync();
            // Map and Return

            return mapper.Map<OrderResultDTO>(order);

        }

        

        public async Task<IEnumerable<DeliveryMethodResultDTO>> GetDeliveryMethodAsync()
        {

            var methods =await unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();

            return mapper.Map<IEnumerable<DeliveryMethodResultDTO>>(methods);
        }

        public async Task<IEnumerable<OrderResultDTO>> GetOrdersByEmailAsync(string email)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludeSpecifications(email));
            return mapper.Map<IEnumerable<OrderResultDTO>>(orders);

        }

        public async Task<OrderResultDTO> GetOrderByIdAsync(Guid id)
        {
            var order= await unitOfWork.GetRepository<Order,Guid>().GetAsync(new OrderWithIncludeSpecifications(id)) ?? throw new OrderNotFoundException(id);

            return mapper.Map<OrderResultDTO>(order);

        }
    }
}
