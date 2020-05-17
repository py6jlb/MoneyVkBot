using System;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VkApiHandler.Services;
using VkApiHandler.Services.Abstractions;
using VkNet;
using VkNet.Model;
using VkNet.Model.GroupUpdate;

namespace VkApiHandler
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostContext, configLogging) => { configLogging.AddConsole(); })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<HostOptions>(option =>
                {
                    option.ShutdownTimeout = TimeSpan.FromSeconds(35);
                });
                services.AddSingleton(sp => Channel.CreateUnbounded<GroupUpdate>());
                services.AddSingleton(sp => {
                    var config = sp.GetService<IConfiguration>();
                    var token = config["VK_BOT_TOKEN"] ?? throw new ArgumentNullException("token", "Параметр не может быть null, проверьте перменные окружения.");
                    var vkApi = new VkApi();
                    vkApi.Authorize(new ApiAuthParams() { AccessToken = token });
                    return vkApi;
                });
                services.AddTransient<IVkApiService, VkApiService>();
                services.AddTransient<IMessageService, MessageService>();
                services.AddTransient<IKeyboardService, KeyboardService>();
                services.AddHostedService<UpdateMonitoringService>();
                services.AddHostedService<HandleUpdateService>();
            });
    }
}
