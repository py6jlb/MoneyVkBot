using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using VkBot.Services.Adstractions.BotServices;

namespace VkBot.Services.Implementations.BotServices
{
    public class BotWorkerService : IHostedService
    {
        private readonly IBotService _botService;

        public BotWorkerService(IBotService botService)
        {
            _botService = botService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _botService.StartBot();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}