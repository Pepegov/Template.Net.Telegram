using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pepegov.MicroserviceFramework.Definition;
using Serilog;
using Serilog.Events;
using Template.Net.TelegramBot.Definitions;

namespace Template.Net.TelegramBot;

public class Program
{
    private static void Main(string[] args)
    {
        //Configure logging
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        
        //Host
        CreateHostBuilder(args)
            .ConfigureSerilog()
            .Build()
            .Run();    //Configure logging
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, service) =>
            {
                service.AddApplicationDefinitions(context.Configuration, typeof(Program).Assembly);
                service.AddHostedService<Worker>();
            });
}