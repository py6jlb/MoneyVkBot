using System;
using VkBot.Services.Adstractions.BotServices;

namespace VkBot.Services.Implementations.BotServices
{
    public class ReadMessagesService : IReadMessagesService
    {
        public void DoSomethingUseful(string text)
        {
            Console.WriteLine($"I Am service {text}");
        }
    }
}