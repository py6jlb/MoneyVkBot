using System.Threading.Tasks;
using VkNet.Model.GroupUpdate;

namespace VkApiHandler.Services.Abstractions
{
    public interface IMessageService
    {
        Task SendMessageWithCategoryKeyboard(GroupUpdate updateObj, string text);
        Task SendMessageWithoutKeyboard(GroupUpdate updateObj, string text);
        Task SendEchoMessage(GroupUpdate updateObj, bool isStart);
    }
}