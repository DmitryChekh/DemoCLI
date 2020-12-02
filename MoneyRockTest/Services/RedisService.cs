using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MoneyRockTest.Services
{
    public class RedisService
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SetString(string key, string value)
        {
            var result = await _database.StringSetAsync(key, value);
            if(result)
                Console.WriteLine($"Добавлено:\n[\n{key}: {value}\n]");
            else
                Console.WriteLine("Не добавлено");
        }

        public async Task GetString(string key)
        {

            var valueOfKey = await _database.StringGetAsync(key);

            if(valueOfKey.IsNullOrEmpty)
                Console.WriteLine("Key is not found");
            else
                Console.WriteLine(valueOfKey.ToString());
        }


    }
}
