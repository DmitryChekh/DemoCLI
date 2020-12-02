using CommandLine;
using MoneyRockTest.Options;
using MoneyRockTest.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyRockTest
{
    public class CommandExecuter
    {
        //private readonly PostgresService _postgresService;
        //private readonly RabbitConsumerService _rabbitConsumerService;
        //private readonly RabbitProducerService _rabbitProducerService;
        //private readonly RedisService _redisService;

        public CommandExecuter()
        {
            //_postgresService = new PostgresService();
            //_rabbitConsumerService = new RabbitConsumerService();
            //_rabbitProducerService = new RabbitProducerService();
            //_redisService = new RedisService();
        }



        public void Execute(string[] args)
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
                          await postgresService.GetMessageById(id);
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
                      await postgresService.AddMessage(o.Value);
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
              if (o.Receive)
              {
                  RabbitConsumerService rabbitConsumerService = new RabbitConsumerService();
                  rabbitConsumerService.Subscribe();
              }
          });
        }

    }
}
