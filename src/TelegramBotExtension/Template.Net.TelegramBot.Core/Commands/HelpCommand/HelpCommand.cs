using Microsoft.Extensions.Logging;
using System.Text;
using CommandLine;
using Telegram.Bot;
using Telegram.Bot.Types;
using Template.Net.TelegramBot.Core.Commands.Base;

namespace Template.Net.TelegramBot.Core.Commands.HelpCommand
{
    [Command("/help")]
    public class HelpCommand : ICommandHandler
    {
        private readonly ILogger<HelpCommand> _logger;

        public HelpCommand(ILogger<HelpCommand> logger)
        {
            _logger = logger;
        }
        
        [Obsolete("Obsolete")]
        public async Task HandleAsync(CommandContext context)
        {
            if (context.Update.Message is null)
            {
                return;
            }
            
            if (context.Message.Arguments is null || context.Message.Arguments.Count == 0)
            {
                await context.BotClient.SendTextMessageAsync(context.Update.Message.Chat.Id, 
                    "This is the help command.\n\r You can run this command with the arguments --chatid or --userid", cancellationToken: context.CancellationToken);
            }

            var arguments = await ParseArguments(context);

            if (arguments is null)
            {
                return;
            }
            
            if (arguments.IsChatId)
            {
                await context.BotClient.SendTextMessageAsync(context.Update.Message.Chat.Id,
                    $"Chat id {context.Update.Message.Chat.Id}", cancellationToken: context.CancellationToken);
            }
            if (arguments.IsUserId)
            {
                await context.BotClient.SendTextMessageAsync(context.Update.Message.Chat.Id,
                    $"Your user id {context.Update.Message?.From?.Id}", cancellationToken: context.CancellationToken);
            }

            var argumentCount = context.Message.Arguments?.Count ?? 0;
            StringBuilder stringBuilder = new StringBuilder(argumentCount);
            context.Message.Arguments?.ForEach(x => stringBuilder.Append(x + " "));
            _logger.LogInformation($"Command {context.Message.Command} is executed with arguments {stringBuilder}");
        }

        [Obsolete("Obsolete")]
        private async Task<HelpArguments?> ParseArguments(CommandContext context)
        {
            var parserResult = Parser.Default.ParseArguments<HelpArguments?>(context.Message.Arguments);

            return await parserResult.MapResult(Task.FromResult,
                async errors =>
                {
                    if (context.Update.Message is null)
                    {
                        return null;
                    }
                    
                    var errorText = string.Join("\n", errors);
                    await context.BotClient.SendTextMessageAsync(context.Update.Message.Chat.Id, errorText, cancellationToken: context.CancellationToken);
                    _logger.LogError(errorText);

                    return null;
                });
        }
    }
}
