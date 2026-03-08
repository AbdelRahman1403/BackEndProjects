using Shared.Dtos.BasketDots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer.IServices
{
    public interface IBasketServices
    {
        Task<BasketDto> GetBasketAsync(string Key);

        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto Item);

        Task<bool> DeleteBasketAsync(string Key);
    }
}
