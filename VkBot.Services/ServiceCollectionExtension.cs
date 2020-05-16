using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VkBot.Services.Adstractions.BotServices;
using VkBot.Services.Implementations.BotServices;
using VkNet;
using VkNet.Model;

namespace VkBot.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddVkApi(this IServiceCollection services)
        {
            services.AddSingleton<VkApi>(sp=>{
                var config = sp.GetService<IConfiguration>();
                var token = config["VK_BOT_TOKEN"] ?? throw new ArgumentNullException("token", "Параметр не может быть null, проверьте перменные окружения.");
                var vkapi = new VkApi();
                vkapi.Authorize(new ApiAuthParams() { AccessToken = token });
                return vkapi;
            });
            return services;
        }

        public static IServiceCollection AddBotService(this IServiceCollection services){
            services.AddVkApi();
            services.AddTransient<IKeyboardService, KeyboardService>();
            services.AddTransient<ISendMessagesService, SendMessagesService>();
            services.AddTransient<IReadMessagesService, ReadMessagesService>();
            services.AddTransient<IVkApiService, VkApiService>();
            services.AddTransient<IBotService, BotService>();
            services.AddHostedService<BotWorkerService>();
            return services;
        }
    }
}