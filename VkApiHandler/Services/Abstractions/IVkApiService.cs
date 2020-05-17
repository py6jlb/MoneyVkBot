using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Model;
using VkNet.Model.GroupUpdate;

namespace VkApiHandler.Services.Abstractions
{
    public interface IVkApiService
    {
        Task CheckUpdates();
        Task<BotsLongPollHistoryResponse> GetHistoryAsync();
        Task HandleUpdates(CancellationToken stoppingToken);
        //Task HandleUpdates(IEnumerable<GroupUpdate> updates);
    }
}