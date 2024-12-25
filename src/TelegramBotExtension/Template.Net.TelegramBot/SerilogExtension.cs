using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Template.Net.TelegramBot
{
    public static class SerilogExtension
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder) => builder
            .ConfigureLogging((loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
            })
            .UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
    }
}
