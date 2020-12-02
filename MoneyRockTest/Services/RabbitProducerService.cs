using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MoneyRockCLI.Services
{
    public class RabbitProducerService
    {
        private readonly IConnectionFactory _factory;

        public RabbitProducerService()
        {
            _factory = new ConnectionFactory() {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Send(string message)
        {

            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "myqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "myqueue", basicProperties: null, body: body);
                Console.WriteLine("Sent {0}", message);
            }
        }

    }
}
