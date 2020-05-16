using System;
using VkBot.Services.Adstractions.BotServices;
using Microsoft.Extensions.Configuration;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;

namespace VkBot.Services.Implementations.BotServices
{
    public class VkApiService : IVkApiService
    {
        private readonly VkApi _vkapi;
        private readonly IConfiguration _config;
        private readonly ISendMessagesService _sendMessageService;
        private readonly string _groupId;

        public VkApiService(VkApi vkapi, IConfiguration config ,ISendMessagesService sendMessageService)
        {
            _vkapi = vkapi;
            _config = config;
            _sendMessageService = sendMessageService;
            _groupId = _config["VK_BOT_GROUP_ID"] ?? throw new ArgumentNullException(nameof(_groupId), "Параметр не может быть null, проверьте перменные окружения.");
        }

        public BotsLongPollHistoryParams GetHistoryParams()
        {
            var serverResponse = _vkapi.Groups.GetLongPollServer(ulong.Parse(_groupId));
            return new BotsLongPollHistoryParams()
            {
                Server = serverResponse.Server,
                Ts = serverResponse.Ts,
                Key = serverResponse.Key,
                Wait = 5
            };
        }

        public BotsLongPollHistoryResponse GetHistory()
        {
            var historyParams = GetHistoryParams();
            var history = _vkapi.Groups.GetBotsLongPollHistory(historyParams);
            return history;
        }

        public bool HistoryHasUpdtes(BotsLongPollHistoryResponse history)
        {
            return history?.Updates != null ? true : false;
        }

        public void HandleHistoryUpdate(BotsLongPollHistoryResponse history)
        {
            foreach (var updateObj in history.Updates)
            {
                if (updateObj.Type == GroupUpdateType.MessageNew)
                {
                    DispatchUpdates(updateObj);
                }
            }
        }

        public void DispatchUpdates(GroupUpdate updateObj){
            switch(updateObj.Message.Text){
                case "Начать":
                    _sendMessageService.SendEchoMessage(updateObj, true);
                    break;
                default:
                    _sendMessageService.SendEchoMessage(updateObj, false);
                    break;
            }
        }


    }
}