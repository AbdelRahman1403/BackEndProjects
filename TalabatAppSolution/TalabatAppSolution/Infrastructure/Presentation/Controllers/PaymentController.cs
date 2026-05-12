using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer.IServices;
using Shared.Dtos.BasketDots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class paymentController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentAtent(string basketId)
        {
            var basket = await serviceManager.paymentServices.CreateOrUpdatePaymentIntent(basketId);
            return Ok(basket);
        }
    }
}
