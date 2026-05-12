using Shared.Dtos.AuthenticationDtos;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer.IServices
{
    public interface IOrderServices
    {
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email);
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsycn();

        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersOfUserAsync(string email);
        Task<OrderToReturnDto> GetOrderByIdOfUserAsync(Guid Id);
    }
}
