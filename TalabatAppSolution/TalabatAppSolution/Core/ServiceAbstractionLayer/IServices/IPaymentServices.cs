using Shared.Dtos.BasketDots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer.IServices
{
    public interface IPaymentServices
    {
        Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId);
    }
}
