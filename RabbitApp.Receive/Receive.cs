using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitApp.Receive
{
    public class Receive: IReceive
    {
        public async Task StartReceivingAsync()
        {
            var factory = new ConnectionFactory();
            factory.VirtualHost = "/"; 
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");

                int dots = message.Split('.').Length - 1;
                await Task.Delay(dots * 1000);

                Console.WriteLine(" [x] Done");
            };

        }
    }
}
