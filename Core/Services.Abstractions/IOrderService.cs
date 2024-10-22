using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService // ay service malhash access 3ala el domain models
    {
        // Get Order By Id ==> OrderResultDTO(int Id)
        public Task<OrderResultDTO> GetOrderByIdAsync(Guid id);

        //Get Orders for user by Email ==> IEnumerable<OrderResltDTO>(string Email)
        public Task<IEnumerable<OrderResultDTO>> GetOrdersByEmailAsync(string email);


        //Create Order==> OrderResultDTO(OrderRequest(Basket,Address,DeliveryMethod   ), string email
        public Task<OrderResultDTO> CreateOrderAsync(OrderRequest request,string userEmail);


        //Get All Delivery Methods  

        public Task<IEnumerable<DeliveryMethodResultDTO>> GetDeliveryMethodAsync();

    }
}
