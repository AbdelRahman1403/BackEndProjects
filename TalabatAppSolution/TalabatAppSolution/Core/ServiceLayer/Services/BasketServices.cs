using AutoMapper;
using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Exceptions.BasketExceptions;
using DomainLayer.Models.BasketModels;
using ServiceAbstractionLayer.IServices;
using Shared.Dtos.BasketDots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BasketServices(IBasketRepo basketRepo ,IMapper mapper) : IBasketServices
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto Item)
        {
            var BasketMap = mapper.Map<BasketDto , CustomerBasket>(Item);

            var SavedBasket = await basketRepo.CreateOrUpdateBasketAsync(BasketMap);

            if(SavedBasket is not null)
            {
                return await GetBasketAsync(SavedBasket.Id);
            }
            else
            {
                throw new Exception("Something went wrong while saving the basket");
            }
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await basketRepo.DeleteBasketAsync(Key);
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await basketRepo.GetBasketAsync(Key);

            if(Basket is not null)
            {
                return mapper.Map<CustomerBasket , BasketDto>(Basket);
            }
            else
            {
                throw new BasketNotFoundException(Key);
            }
        }
    }
}
