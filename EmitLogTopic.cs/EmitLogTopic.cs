using RabbitMQ.Client;
using System.Text;

namespace EmitLogTopic.cs
{
    public class EmitLogTopic
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


            await channel.ExchangeDeclareAsync(exchange: "topic_logs", type: ExchangeType.Topic);

            var severity = (args.Length > 0) ? args[0] : "nonymous.info";
            var message = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: "topic_logs", routingKey: severity, body: body);
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
