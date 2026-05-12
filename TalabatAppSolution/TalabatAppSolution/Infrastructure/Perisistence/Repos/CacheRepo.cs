using DomainLayer.ContractsRepoInterfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perisistence.Repos
{
    public class CacheRepo(IConnectionMultiplexer connection) : ICacheRepo
    {
        private IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string CacheKey)
        {
            var CacheValue = await _database.StringGetAsync(CacheKey);

            return CacheValue.IsNullOrEmpty ? null : CacheValue.ToString();
        }

        public async Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive);
        }
    }
}
