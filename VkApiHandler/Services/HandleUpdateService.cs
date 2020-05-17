using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VkApiHandler.Services.Abstractions;

namespace VkApiHandler.Services
{
    public class HandleUpdateService : BackgroundService
    {
        private readonly IVkApiService _apiService;
        private readonly ILogger<HandleUpdateService> _logger;

        public HandleUpdateService(IVkApiService apiService, ILogger<HandleUpdateService> logger)
        {
            _logger = logger;
            _apiService = apiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Updates handler is starting.");

            stoppingToken.Register(() => _logger.LogDebug("#2 Update handler background task is stopping."));
            _logger.LogDebug("Update handler background task is doing background work.");
            await _apiService.HandleUpdates(stoppingToken);
            _logger.LogDebug("Update handler background task is stopping.");
        }
    }
}