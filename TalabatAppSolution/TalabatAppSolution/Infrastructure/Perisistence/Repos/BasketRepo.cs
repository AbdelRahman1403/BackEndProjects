using DomainLayer.ContractsRepoInterfaces;
using DomainLayer.Models.BasketModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Perisistence.Repos
{
    public class BasketRepo(IConnectionMultiplexer connection) : IBasketRepo
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var ChickIsCreatedOrUpdate = await _database.StringSetAsync(basket.Id, JsonBasket, (TimeToLive == null ? TimeSpan.FromHours(5) : TimeSpan.FromHours(10)));

            if (ChickIsCreatedOrUpdate)
                return await GetBasketAsync(basket.Id);
            else
                return null;

        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _database.KeyDeleteAsync(Key);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var Basket = await _database.StringGetAsync(Key);

            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }
    }
}
