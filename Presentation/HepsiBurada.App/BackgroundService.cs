using HepsiBurada.Service.CampaignService;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace HepsiBurada.App
{
    public class BackgroundService : IHostedService, IDisposable
    {
        private Timer _timerOrderManager;
        private Timer _timerCampaignManager;
        public void Dispose()
        {
            _timerOrderManager?.Dispose();
            _timerCampaignManager?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timerCampaignManager = new Timer(EndedCampaignsControl, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void EndedCampaignsControl(object state)
        {
            var campaignService = Startup.ServiceProvider.GetService<ICampaignService>();
            campaignService.EndCampaignsByEndDate();
        }
    }
}
