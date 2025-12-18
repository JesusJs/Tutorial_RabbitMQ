using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitApp.Send
{
    public class SendWorker : BackgroundService
    {
        private readonly ISend _sender;

        public SendWorker(ISend sender)
        {
            _sender = sender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _sender.ConnectionAsync();
                await Task.Delay(0, stoppingToken); 
            }
        }
    

    }
}
