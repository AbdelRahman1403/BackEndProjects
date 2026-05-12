using DomainLayer.ContractsRepoInterfaces;
using ServiceAbstractionLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CacheServices(ICacheRepo repo) : ICacheServices
    {
        public async Task<string?> GetAsync(string CacheKey)
        {
            return await repo.GetAsync(CacheKey);
        }

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var Value = JsonSerializer.Serialize(CacheValue);

            await repo.SetAsync(CacheKey, Value, TimeToLive);
        }
    }
}
