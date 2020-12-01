using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MoneyRockTest
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

        public void SetString(string key, string value)
        {
            var result = _database.StringSet(key, value);
            if(result)
                Console.WriteLine($"Добавлено:\n[\n{key}: {value}\n]");
            else
                Console.WriteLine("Не добавлено");
        }

        public void GetString(string key)
        {
            var valueOfKey = _database.StringGet(key);
            Console.WriteLine(valueOfKey.ToString());
        }
        


    }
}
