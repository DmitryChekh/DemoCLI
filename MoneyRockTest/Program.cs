using System;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MoneyRockTest
{
    class Program
    {
        public static bool keepRunnig = true;
        static async Task Main(string[] args)
        {
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
                e.Cancel = true;
                Program.keepRunnig = false;
            };
 

            RedisService redisService = new RedisService();
            PostgresService postgresService = new PostgresService();

            while(Program.keepRunnig)
            {
                var command = Console.ReadLine();

                switch(command)
                {
                    case "/redis":
                        Console.WriteLine("use redis service");
                        break;
                    case "/postgres":
                        
                        Console.WriteLine(await postgresService.ReadAsync("dad")); 
                        Console.WriteLine("use postgres service");
                        break;
                    case "/rabit":
                        Console.WriteLine("use rabit servce");
                        break;
                }
            }

            Console.WriteLine("Exited...");
        }



    }
}
