using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreService.DataAccess;
using StoreService.DataAccess.Abstractions;
using StoreService.Services;

namespace StoreService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<LiteDbOptions>(Configuration.GetSection("LiteDbOptions"));
            services.AddSingleton<ILiteDbContext, LiteDbContext>();
            services.AddTransient<IDataReaderService, DataReaderService>();
            services.AddTransient<IDataWriterService, DataWriterService>();
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<WriterService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Связь с конечными точками gRPC должна осуществляться через клиент gRPC. Чтобы узнать, как создать клиент, посетите страницу https://go.microsoft.com/fwlink/?linkid=2086909.");
                });
            });
        }
    }
}
