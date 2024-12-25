using Telegram.Bot.Types;
using Telegram.Bot;

namespace Template.Net.TelegramBot.Core.Commands.Base
{
    /// <summary>
    /// Interface for all command handlers
    /// </summary>
    public interface ICommandHandler 
    {
        Task HandleAsync(CommandContext context);
    }
}
