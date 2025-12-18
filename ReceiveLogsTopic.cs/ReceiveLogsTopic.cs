using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ReceiveLogsTopic.cs
{
    public class ReceiveLogsTopic
    {
        public static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                                        Environment.GetCommandLineArgs()[0]);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                Environment.ExitCode = 1;
                return;
            }

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: "topic_logs", type: ExchangeType.Topic);
            QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
            string queueName = queueDeclareResult.QueueName;

            foreach (string? bindingKey in args)
            {
                await channel.QueueBindAsync(queue: queueName, exchange: "topic_logs", routingKey: bindingKey);
            }

            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] {message}");
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
