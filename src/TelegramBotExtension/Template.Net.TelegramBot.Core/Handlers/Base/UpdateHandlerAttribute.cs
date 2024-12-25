using Telegram.Bot.Types.Enums;

namespace Template.Net.TelegramBot.Core.Handlers.Base
{
    /// <summary>
    /// Attribute by which the factory will search for the handler
    /// </summary>
    public class UpdateHandlerAttribute(UpdateType updateType) : Attribute
    {
        public UpdateType UpdateType { get; } = updateType;
    }
}
