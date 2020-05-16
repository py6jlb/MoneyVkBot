using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkBot.Services.Adstractions.BotServices
{
    public interface IVkApiService
    {
        BotsLongPollHistoryParams GetHistoryParams();
        BotsLongPollHistoryResponse GetHistory();
        bool HistoryHasUpdtes(BotsLongPollHistoryResponse history);
        void HandleHistoryUpdate(BotsLongPollHistoryResponse history);
    }
}