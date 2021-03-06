﻿using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;

namespace Utilities.Services
{
    public class RabbitConsumerService : IRabbitConsumerService
    {
        private readonly IConnectionFactory _factory;

        public RabbitConsumerService()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Subscribe()
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "myqueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    Thread.Sleep(5000);
                };

                channel.BasicConsume(queue: "myqueue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.ReadLine();
            }
        }

    }
}
