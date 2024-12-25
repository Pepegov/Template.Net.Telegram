using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Template.Net.TelegramBot.Core.Commands.Base;
using Template.Net.TelegramBot.Core.Handlers.Base;

namespace Template.Net.TelegramBot.Core.Handlers
{
    /// <summary>
    /// Executes text commands
    /// </summary>
    public class CommandHandler : IApplicationUpdateHandler
    {
        private readonly ILogger<CommandHandler> _logger;
        private readonly CommandHandlerParser _commandHandlerParser;

        public CommandHandler(ILogger<CommandHandler> logger, CommandHandlerParser commandHandlerParser)
        {
            _logger = logger;
            _commandHandlerParser = commandHandlerParser;
        }

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var splitText = update.Message?.Text?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var commandName = splitText[0];
            var commandArguments = splitText.Skip(1).ToList();
            for (int i = 0; i < commandArguments.Count(); i++)
            {
                commandArguments[i] = commandArguments[i].Replace("—", "--");
            }

            var userCommand = new UserCommand
            {
                Command = commandName,
                Arguments = commandArguments
            };

            if (_commandHandlerParser.TryParse(commandName, out var command))
            {
                var context = new CommandContext(botClient, update, userCommand, cancellationToken);
                await command.HandleAsync(context);
            }
        }
    }
}
