using VkNet.Model.Keyboard;

namespace VkBot.Services.Adstractions.BotServices
{
    public interface IKeyboardService
    {
         MessageKeyboard GetGlobalKeyboard();
    }
}