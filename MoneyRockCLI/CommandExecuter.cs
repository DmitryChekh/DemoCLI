using CommandLine;
using MoneyRockCLI.Options;
using Utilities.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyRockCLI
{
    public class CommandExecuter : ICommandExecuter
    {
        private readonly IRedisService _redisService;
        private readonly IPostgresService _postgresService;
        private readonly IRabbitConsumerService _rabbitConsumerService;
        private readonly IRabbitProducerService _rabbitProducerService;
        public CommandExecuter(
            IRedisService redisService,
            IPostgresService postgresService,
            IRabbitProducerService rabbitProducerService,
            IRabbitConsumerService rabbitConsumerService
            )
        {
            _redisService = redisService;
            _rabbitConsumerService = rabbitConsumerService;
            _rabbitProducerService = rabbitProducerService;
            _postgresService = postgresService;
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
            if (!string.IsNullOrEmpty(option.Key))
            {
                if (option.Read)
                {

                    await _redisService.GetString(option.Key);
                }
                if (option.Write)
                {
                    if (!string.IsNullOrEmpty(option.Value))
                    {

                        await _redisService.SetString(option.Key, option.Value);
                    }
                }
            }
        }

        private async Task ExecutePostgresServiceAsync(PostgresOption option)
        {
            Console.WriteLine("Start working with PostgreSQL...");
            if (option.Read)
            {
                if (option.Id)
                {
                    int id;
                    var result = Int32.TryParse(option.Value, out id);
                    if (result)
                        await _postgresService.GetMessageById(id);
                    else
                        Console.WriteLine("Value is not valid");
                }
                else
                    await _postgresService.GetMessage(option.Value);
            }
            if (option.Write)
            {
                if (!string.IsNullOrEmpty(option.Value))
                {
                    await _postgresService.AddMessage(option.Value);
                }
            }
        }

        private void ExecuteRabbitService(RabbitOption option)
        {
            Console.WriteLine("Start working with RabbitMQ...");

            if (option.Send)
            {
                if (!string.IsNullOrEmpty(option.Value))
                {
                    _rabbitProducerService.Send(option.Value);
                }
            }
            if (option.Receive)
            {
                _rabbitConsumerService.Subscribe();
            }
        }

    }
}
