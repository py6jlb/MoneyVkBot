using VkNet.Model.GroupUpdate;

namespace VkBot.Services.Adstractions.BotServices
{
    public interface ISendMessagesService
    {
         void SendEchoMessage(GroupUpdate updateObj, bool isStart);
    }
}