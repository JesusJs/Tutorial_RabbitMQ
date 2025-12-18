using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
namespace RabbitApp.Receive
{

    public class ReceiveWorker : BackgroundService
    {
        private readonly IReceive _receiver;

        public ReceiveWorker(IReceive receiver)
        {
            _receiver = receiver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.StartReceivingAsync();
        }
    }

}
