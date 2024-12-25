using CommandLine;

namespace Template.Net.TelegramBot.Core.Commands.HelpCommand
{
    /// <summary>
    /// Represents the arguments for the command-line parser.
    /// </summary>
    public class HelpArguments
    {
        [Option('c', "chatid", Required = false)]
        public bool IsChatId { get; set; }

        [Option('u', "userid", Required = false)]
        public bool IsUserId { get; set; }
    }
}
