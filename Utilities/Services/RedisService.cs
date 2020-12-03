using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Utilities.Services
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public RedisService()
        {
            try
            {
                _redis = ConnectionMultiplexer.Connect("localhost:6379");
                _database = _redis.GetDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<string> SetString(string key, string value)
        {
            var result = await _database.StringSetAsync(key, value);
            if (result)
                return $"Added:\n[{key}: {value}]";
            else
                return "Can't added. Something wrong";
        }

        public async Task<string> GetString(string key)
        {

            var valueOfKey = await _database.StringGetAsync(key);

            if (valueOfKey.IsNullOrEmpty)
                return "Key is not found";
            else
                return valueOfKey.ToString();
        }

    }
}
