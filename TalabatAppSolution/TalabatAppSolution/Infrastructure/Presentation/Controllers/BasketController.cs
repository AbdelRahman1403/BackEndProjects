using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
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
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase 
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var Basket = await serviceManager.BasketServices.GetBasketAsync(Key);

            return Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto Item)
        {
            var Basket = await serviceManager.BasketServices.CreateOrUpdateBasketAsync(Item);
            return Ok(Basket);
        }
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result = await serviceManager.BasketServices.DeleteBasketAsync(key);
            return Ok(result);
        }
    }
}
