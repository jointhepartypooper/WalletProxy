using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace WalletProxy
{
    public class WalletAsWebserviceService : IHostedService
    {
        private readonly Container container;

        public WalletAsWebserviceService(Container container)
        {
            this.container = container;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }

}
