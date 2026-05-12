using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer.IServices;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderdto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var Order = await serviceManager.OrderServices.CreateOrderAsync(orderdto, Email);

            return Ok(Order);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await serviceManager.OrderServices.GetAllDeliveryMethodsAsycn();
            return Ok(DeliveryMethods);
        }
        [HttpGet("AllOrders")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrders()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await serviceManager.OrderServices.GetAllOrdersOfUserAsync(Email);
            return Ok(Orders);
        }
    }
}
