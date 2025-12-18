using RabbitMQ.Client;

namespace RabbitApp.Host
{
    public static class ConfigRabbitMQ
    {
        public static ConnectionFactory ConfigRabbitMQLogin()
        {
            var factory = new ConnectionFactory();

            factory.HostName = "localhost";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            return factory;
        }
    }
}
