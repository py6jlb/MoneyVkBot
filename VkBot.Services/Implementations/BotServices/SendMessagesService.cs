using System;
using VkBot.Services.Adstractions.BotServices;
using VkNet;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;

namespace VkBot.Services.Implementations.BotServices
{
    public class SendMessagesService : ISendMessagesService
    {
        private readonly VkApi _vkapi;
        private readonly IKeyboardService _keyboards;

        public SendMessagesService(VkApi vkapi, IKeyboardService keyboards)
        {
            _vkapi = vkapi;
            _keyboards = keyboards;
        }
        public void SendEchoMessage(GroupUpdate updateObj, bool isStart)
        {
            var msg = new MessagesSendParams()
            {
                UserId = updateObj.Message.UserId,
                Message = updateObj.Message.Text,
                PeerId = updateObj.Message.PeerId,
                RandomId = new DateTime().Millisecond
            };

            if(isStart)
                msg.Keyboard = _keyboards.GetGlobalKeyboard();

            _vkapi.Messages.Send(msg);
        }
    }
}