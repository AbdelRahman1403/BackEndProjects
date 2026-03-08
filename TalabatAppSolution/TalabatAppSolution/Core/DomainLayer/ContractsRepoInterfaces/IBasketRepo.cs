using DomainLayer.Models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ContractsRepoInterfaces
{
    public interface IBasketRepo
    {
        Task<CustomerBasket?> GetBasketAsync(string Key);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket , TimeSpan? TimeToLive = null);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
