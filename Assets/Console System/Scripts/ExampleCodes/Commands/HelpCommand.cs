using System.Collections.Generic;
using System.Text;
using jcsilva.ConsoleSystem.Core;

namespace jcsilva.ConsoleSystem.Commands
{
    public class HelpCommand : IConsoleCommand
    {
        public string Name => "help";
        public string Description => "Lists all available commands";
        public string Usage => "help [command]";

        private readonly IReadOnlyDictionary<string, IConsoleCommand> _commands;

        public HelpCommand(IReadOnlyDictionary<string, IConsoleCommand> commands)
        {
            _commands = commands;
        }


        public void Execute(string[] args, IConsoleOutput output)
        {
            var commands = _commands;

            // help <specific command>
            if (args.Length > 0)
            {
                var target = args[0].ToLower();
                if (commands.TryGetValue(target, out var cmd))
                {
                    output.Log($"{cmd.Name} — {cmd.Description}");
                    output.Log($"Usage: {cmd.Usage}");
                }
                else
                {
                    output.Log($"No command found: '{target}'", LogType.Error);
                }
                return;
            }

            // help (list all)
            var sb = new StringBuilder();
            sb.AppendLine("Available commands:");
            foreach (var cmd in commands.Values)
            {
                sb.AppendLine($"  {cmd.Name,-15} {cmd.Description}");
            }
            output.Log(sb.ToString());
        }
    }
}