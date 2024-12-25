namespace Template.Net.TelegramBot.Core.Commands.Base
{
    /// <summary>
    /// Helps to pass the command
    /// </summary>
    public class UserCommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Command { get; set; } = null!;
        /// <summary>
        /// Command argumnets
        /// </summary>
        public List<string>? Arguments { get; set; }
    }
}
