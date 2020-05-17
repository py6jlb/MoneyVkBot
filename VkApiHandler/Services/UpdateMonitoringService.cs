using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VkApiHandler.Services.Abstractions;

namespace VkApiHandler.Services
{
    public class UpdateMonitoringService : BackgroundService
    {
        private readonly IVkApiService _apiService;
        private readonly ILogger<UpdateMonitoringService> _logger;

        public UpdateMonitoringService(IVkApiService apiService, ILogger<UpdateMonitoringService> logger)
        {
            _logger = logger;
            _apiService = apiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Update monitoring is starting.");

            stoppingToken.Register(() => _logger.LogDebug("#1 Update monitoring background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Update monitoring background task is doing background work.");
                await _apiService.CheckUpdates();
            }

            _logger.LogDebug("Update monitoring background task is stopping.");
            await Task.CompletedTask;
        }
    }
}