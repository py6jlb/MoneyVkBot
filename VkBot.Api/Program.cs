using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VkBot.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        Console.Write(Directory.GetCurrentDirectory());
                        config.AddJsonFile(Path.Combine("Configurations", "appsettings.json"), optional: true, reloadOnChange: true)
                            .AddJsonFile(Path.Combine("Configurations", $"appsettings.{env.EnvironmentName}.json"), optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables(prefix: "APP_");
                        config.AddCommandLine(args);
                    });
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                });
    }
}
