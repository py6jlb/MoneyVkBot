using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Model;
using VkNet.Model.GroupUpdate;

namespace VkApiHandler.Services.Abstractions
{
    public interface IVkApiService
    {
        Task RunLongPoolingTask(CancellationToken stoppingToken);
        Task<BotsLongPollHistoryResponse> GetHistoryAsync();
        Task HandleUpdates(IEnumerable<GroupUpdate> updates);
    }
}