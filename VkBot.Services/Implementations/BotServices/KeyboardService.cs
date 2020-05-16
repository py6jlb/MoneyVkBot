using VkBot.Services.Adstractions.BotServices;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot.Services.Implementations.BotServices
{
    public class KeyboardService : IKeyboardService
    {
        public MessageKeyboard GetGlobalKeyboard()
        {
            var keyboard = new KeyboardBuilder()
                .AddButton("Подтвердить", "кнопка1", KeyboardButtonColor.Primary, "text")
                .SetInline(false)
                .AddLine()
                .AddButton("Отменить", "кнопка2", KeyboardButtonColor.Primary, "text")
                .Build();
            
            return keyboard;
        }
    }
}