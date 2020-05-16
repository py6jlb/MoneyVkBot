using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VkApiHandler.Services.Abstractions;

namespace VkApiHandler.Services
{
    public class BotService : BackgroundService
    {
        private readonly IVkApiService _apiService;
        private readonly ILogger<BotService> _logger;

        public BotService(IVkApiService apiService, ILogger<BotService> logger)
        {
            _logger = logger;
            _apiService = apiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _apiService.RunLongPoolingTask(stoppingToken);
        }
    }
}