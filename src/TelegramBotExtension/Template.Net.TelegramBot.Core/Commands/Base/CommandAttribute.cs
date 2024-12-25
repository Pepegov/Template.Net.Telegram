namespace Template.Net.TelegramBot.Core.Commands.Base
{
    /// <summary>
    /// The attribute in which the text representation of the command is written
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandAttribute(string command) : Attribute
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Command { get; set; } = command;
    }
}
