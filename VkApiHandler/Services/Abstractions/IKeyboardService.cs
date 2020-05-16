using VkNet.Model.Keyboard;

namespace VkApiHandler.Services.Abstractions
{
    public interface IKeyboardService
    {
        MessageKeyboard GetCategoryKeyboard();
        MessageKeyboard GetGlobalKeyboard();
        MessageKeyboard GetEmptyKeyboard();
    }
}