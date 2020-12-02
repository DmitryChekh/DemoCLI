using System;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using CommandLine.Text;
using CommandLine;
using MoneyRockTest.Options;
using MoneyRockTest.Services;
using LinqToDB.Data;
using MoneyRockTest.DataModels;

namespace MoneyRockTest
{
    class Program
    {
        public static bool keepRunnig = true;
        static void Main()
        {

            DataConnection.DefaultSettings = new MySettings();


            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                Program.keepRunnig = false;
            };

            while (Program.keepRunnig)
            {
                var command = Console.ReadLine();

                var args = command.Split(' ');

                ParseCommand(args);
            }
            
        }


        static void ParseCommand(string[] args)
        {
            Parser.Default.ParseArguments<RedisOption, PostgresOption, RabbitOption>(args)
           .WithParsed<RedisOption>(async o =>
           {
               Console.WriteLine("Start working with Redis...");
               RedisService redisService = new RedisService();
               if (!string.IsNullOrEmpty(o.Key))
               {
                   if (o.Read)
                   {

                       await redisService.GetString(o.Key);
                   }
                   if (o.Write)
                   {
                       if (!string.IsNullOrEmpty(o.Value))
                       {

                           await redisService.SetString(o.Key, o.Value);
                       }
                   }
               }

           })
           .WithParsed<PostgresOption>(async o =>
           {
               Console.WriteLine("Start working with PostgreSQL...");
               PostgresService postgresService = new PostgresService();
               if (o.Read)
               {
                   if (o.Id)
                   {
                       int id;
                       var result = Int32.TryParse(o.Value, out id);
                       if (result)
                           postgresService.GetMessageById(id);
                       else
                           Console.WriteLine("Value is not valid");
                   }
                   else
                       await postgresService.GetMessage(o.Value);
               }
               if (o.Write)
               {
                   if (!string.IsNullOrEmpty(o.Value))
                   {
                       postgresService.AddMessage(o.Value);
                   }
               }
           })
           .WithParsed<RabbitOption>(o =>
           {
               Console.WriteLine("Start working with RabbitMQ...");

               if (o.Send)
               {
                   RabbitProducerService rabbitProducerService = new RabbitProducerService();
                   if (!string.IsNullOrEmpty(o.Value))
                   {
                       rabbitProducerService.Send(o.Value);
                   }
               }
               if(o.Receive)
               {
                   RabbitConsumerService rabbitConsumerService = new RabbitConsumerService();
                   rabbitConsumerService.Subscribe();
               }
           });
        
        }
    }
}
