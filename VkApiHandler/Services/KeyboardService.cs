using VkApiHandler.Services.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkApiHandler.Services
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

        public MessageKeyboard GetEmptyKeyboard()
        {
            var keyboard = new KeyboardBuilder().Build();
            return keyboard;
        }

        public MessageKeyboard GetCategoryKeyboard()
        {
            var keyboard = new KeyboardBuilder().Build();
            return keyboard;
        }
    }
}