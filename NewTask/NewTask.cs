using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.NewTask
{
    public class NewTask
    {

        public static async Task Main(string[] args)
        {

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            

            await channel.QueueDeclareAsync(queue: "task_queue", durable: true, exclusive: false,
            autoDelete: false, arguments: null);

            var message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties
            {
                Persistent = true
            };


            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "task_queue", mandatory: true,
            basicProperties: properties, body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();


        }

        static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
