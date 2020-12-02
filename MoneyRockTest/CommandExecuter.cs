using CommandLine;
using MoneyRockCLI.Options;
using MoneyRockCLI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyRockCLI
{
    public class CommandExecuter
    {

        public CommandExecuter()
        {
        }



        public void Execute(string[] args)
        {
            Parser.Default.ParseArguments<RedisOption, PostgresOption, RabbitOption>(args)
          .WithParsed<RedisOption>(async option => await ExecuteRedisServiceAsync(option))
          .WithParsed<PostgresOption>(async option => await ExecutePostgresServiceAsync(option))
          .WithParsed<RabbitOption>(option => ExecuteRabbitService(option));

        }


        private async Task ExecuteRedisServiceAsync(RedisOption option)
        {
            Console.WriteLine("Start working with Redis...");
            RedisService redisService = new RedisService();
            if (!string.IsNullOrEmpty(option.Key))
            {
                if (option.Read)
                {

                    await redisService.GetString(option.Key);
                }
                if (option.Write)
                {
                    if (!string.IsNullOrEmpty(option.Value))
                    {

                        await redisService.SetString(option.Key, option.Value);
                    }
                }
            }
        }

        private async Task ExecutePostgresServiceAsync(PostgresOption option)
        {
            Console.WriteLine("Start working with PostgreSQL...");
            PostgresService postgresService = new PostgresService();
            if (option.Read)
            {
                if (option.Id)
                {
                    int id;
                    var result = Int32.TryParse(option.Value, out id);
                    if (result)
                        await postgresService.GetMessageById(id);
                    else
                        Console.WriteLine("Value is not valid");
                }
                else
                    await postgresService.GetMessage(option.Value);
            }
            if (option.Write)
            {
                if (!string.IsNullOrEmpty(option.Value))
                {
                    await postgresService.AddMessage(option.Value);
                }
            }
        }

        private void ExecuteRabbitService(RabbitOption option)
        {
            Console.WriteLine("Start working with RabbitMQ...");

            if (option.Send)
            {
                RabbitProducerService rabbitProducerService = new RabbitProducerService();
                if (!string.IsNullOrEmpty(option.Value))
                {
                    rabbitProducerService.Send(option.Value);
                }
            }
            if (option.Receive)
            {
                RabbitConsumerService rabbitConsumerService = new RabbitConsumerService();
                rabbitConsumerService.Subscribe();
            }
        }

    }
}
