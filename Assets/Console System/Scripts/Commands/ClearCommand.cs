using jcsilva.ConsoleSystem.Core;

namespace jcsilva.ConsoleSystem.Commands
{
    public class ClearCommand : IConsoleCommand
    {
        public string Name => "clear";
        public string Description => "Clears the console output";
        public string Usage => "clear";

        private readonly IConsoleOutput _output;

        public ClearCommand(IConsoleOutput output)
        {
            _output = output;
        }

        public void Execute(string[] args, IConsoleOutput output)
        {
            _output.Clear();
        }
    }
}