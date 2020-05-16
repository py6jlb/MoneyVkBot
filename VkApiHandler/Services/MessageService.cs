using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VkApiHandler.Services.Abstractions;
using VkNet;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;

namespace VkApiHandler.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _looger;
        private readonly VkApi _vkApi;
        private readonly IKeyboardService _keyboards;

        public MessageService(VkApi vkApi, IKeyboardService keyboards, ILogger<MessageService> logger)
        {
            _looger = logger;
            _vkApi = vkApi;
            _keyboards = keyboards;
        }
        public async Task SendEchoMessage(GroupUpdate updateObj, bool isStart)
        {
            var msg = new MessagesSendParams()
            {
                UserId = updateObj.Message.UserId,
                Message = updateObj.Message.Text,
                PeerId = updateObj.Message.PeerId,
                RandomId = new DateTime().Millisecond
            };

            if (isStart)
                msg.Keyboard = _keyboards.GetGlobalKeyboard();

            msg.Keyboard = _keyboards.GetEmptyKeyboard();

            await _vkApi.Messages.SendAsync(msg);
        }

        public async Task SendMessageWithoutKeyboard(GroupUpdate updateObj, string text)
        {
            var msg = new MessagesSendParams
            {
                UserId = updateObj.Message.UserId,
                Message = text,
                PeerId = updateObj.Message.PeerId,
                RandomId = new DateTime().Millisecond,
                Keyboard = _keyboards.GetEmptyKeyboard()
            };
            await _vkApi.Messages.SendAsync(msg);
        }

        public async Task SendMessageWithCategoryKeyboard(GroupUpdate updateObj, string text)
        {
            var msg = new MessagesSendParams
            {
                UserId = updateObj.Message.UserId,
                Message = text,
                PeerId = updateObj.Message.PeerId,
                RandomId = new DateTime().Millisecond,
                Keyboard = _keyboards.GetCategoryKeyboard()
            };
            await _vkApi.Messages.SendAsync(msg);
        }
    }
}