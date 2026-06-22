using ApplicationLayer.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Reposetories
{
    public class CasheReposetory(IConnectionMultiplexer connection) : ICasheReposetory
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var setValue = await _database.StringGetAsync(key);

            return setValue.IsNullOrEmpty ? null : setValue.ToString();
        }

        public async Task SetAsync(string key, string value, TimeSpan expiration)
        {
            await _database.StringSetAsync(key, value, expiration);
        }
    }
}
