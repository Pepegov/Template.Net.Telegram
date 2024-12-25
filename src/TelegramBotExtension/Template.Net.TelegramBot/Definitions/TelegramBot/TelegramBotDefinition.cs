using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Telegram.Bot.Polling;
using Template.Net.TelegramBot.Core;
using Template.Net.TelegramBot.Core.Commands.Base;
using Template.Net.TelegramBot.Core.Handlers.Base;

namespace Template.Net.TelegramBot.Definitions.TelegramBot
{
    public class TelegramBotDefinition : ApplicationDefinition
    {
        public override Task ConfigureServicesAsync(IDefinitionServiceContext context)
        {
            var applicationTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .ToList();
            
            context.ServiceCollection.Configure<TelegramSettings>(context.Configuration.GetSection("Telegram"));

            context.ServiceCollection.AddSingleton<CommandHandlerParser>();
            context.ServiceCollection.AddSingleton<UpdateHandlerManager>();

            var telegramBotUpdateHandlerTypes = applicationTypes.Where(t => t.GetInterfaces().Any(i => i == typeof(IApplicationUpdateHandler)));
            foreach (var telegramBotUpdateHandlerType in telegramBotUpdateHandlerTypes)
            {
                context.ServiceCollection.AddSingleton(telegramBotUpdateHandlerType);
            }

            var commandHandlerTypes = applicationTypes.Where(t => t.GetInterfaces().Any(i => i == typeof(ICommandHandler)));
            foreach (var commandHandlerType in commandHandlerTypes)
            {
                context.ServiceCollection.AddSingleton(commandHandlerType);
            }

            context.ServiceCollection.AddSingleton<IUpdateHandler, UpdateHandler>();
            
            return base.ConfigureServicesAsync(context);
        }
    }
}
