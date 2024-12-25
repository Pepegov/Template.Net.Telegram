using Telegram.Bot;
using Telegram.Bot.Types;

namespace Template.Net.TelegramBot.Core.Commands.Base;

public class CommandContext
{
    public CommandContext(ITelegramBotClient botClient, Update update, UserCommand message, CancellationToken cancellationToken = default)
    {
        BotClient = botClient;
        Update = update;
        Message = message;
        CancellationToken = cancellationToken;
    }

    public ITelegramBotClient BotClient { get; }
    public Update Update { get; }
    public UserCommand Message { get; }
    public CancellationToken CancellationToken { get; }
}