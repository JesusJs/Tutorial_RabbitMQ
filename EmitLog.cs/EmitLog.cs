using RabbitMQ.Client;
using System.Text;

namespace EmitLog.cs
{
    public class EmitLog
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


            await channel.ExchangeDeclareAsync(exchange: "logs", type: ExchangeType.Fanout);

            var message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: "logs", routingKey: string.Empty, body: body);
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
