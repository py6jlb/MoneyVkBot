using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VkApiHandler.Services.Abstractions;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;

namespace VkApiHandler.Services
{
    public class VkApiService : IVkApiService
    {
        private readonly ILogger<VkApiService> _logger;
        private readonly VkApi _vkApi;
        private readonly IMessageService _sendMessageService;
        private readonly ulong _groupId;

        public VkApiService(VkApi vkApi, IConfiguration config, IMessageService sendMessageService, ILogger<VkApiService> logger)
        {
            _logger = logger;
            _vkApi = vkApi;
            _sendMessageService = sendMessageService;
            var groupId = config["VK_BOT_GROUP_ID"] ?? throw new ArgumentNullException(nameof(_groupId), "Параметр не может быть null, проверьте перменные окружения.");
            _groupId = ulong.Parse(groupId);
        }

        public async Task RunLongPoolingTask(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var history = await GetHistoryAsync();
                var updates = GetHistoryUpdates(history);

                if (updates == null || !updates.Any()) continue;

                var _ = Task.Run(()=> HandleUpdates(updates));
            }
        }
        
        public async Task<BotsLongPollHistoryResponse> GetHistoryAsync()
        {
            var connection = await _vkApi.Groups.GetLongPollServerAsync(_groupId);
            var historyParams = GetHistoryParams(connection);
            var history = await _vkApi.Groups.GetBotsLongPollHistoryAsync(historyParams);
            return history;
        }

        public async Task HandleUpdates(IEnumerable<GroupUpdate> updates)
        {
            foreach (var update in updates)
            {
                await DispatchUpdates(update);
            }
        }

        private BotsLongPollHistoryParams GetHistoryParams(LongPollServerResponse connection)
        {
            return new BotsLongPollHistoryParams()
            {
                Server = connection.Server,
                Ts = connection.Ts,
                Key = connection.Key,
                Wait = 30
            };
        }

        private GroupUpdate[] GetHistoryUpdates(BotsLongPollHistoryResponse history)
        {
            return history?.Updates?.Where(x => x.Type == GroupUpdateType.MessageNew).ToArray();
        }

        private bool MessageIsCommand(GroupUpdate updateObj)
        {
            var success = decimal.TryParse(updateObj.Message.Text, out _);
            return !success;
        }

        private async Task DispatchUpdates(GroupUpdate updateObj)
        {
            if (MessageIsCommand(updateObj))
            {
                switch (updateObj.Message.Text)
                {
                    case "Начать":
                        _logger.LogInformation($"Получено комманда: НАЧАТЬ");
                        await _sendMessageService.SendMessageWithoutKeyboard(updateObj, "Добро пожаловать.");
                        break;
                    default:
                        _logger.LogInformation($"Непонятная комманда");
                        await _sendMessageService.SendMessageWithoutKeyboard(updateObj, "Я не понимаю, что от меня хотят(((");
                        break;
                }
            }
            else if(decimal.TryParse(updateObj.Message.Text, out var result))
            {
                _logger.LogInformation($"Получено значение: {result}");
                await _sendMessageService.SendMessageWithoutKeyboard(updateObj, $"Получено значение: {result}");
            }
            else
            {
                _logger.LogInformation($"Вообще непойми что пришло");
                await _sendMessageService.SendMessageWithoutKeyboard(updateObj, "Я не понимаю, что от меня хотят(((");
            }

        }
    }
}