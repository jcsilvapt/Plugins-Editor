using UnityEngine;
using System.Collections.Generic;

namespace jcsilva.ConsoleSystem.Core
{
    public class ConsoleRegistry
    {
        private readonly Dictionary<string, IConsoleCommand> _commands = new Dictionary<string, IConsoleCommand>();
        private IConsoleOutput _output;

        public ConsoleRegistry(IConsoleOutput output)
        {
            _output = output;
        }

        public void Register(IConsoleCommand command)
        {
            var key = command.Name.ToLower();

            if (_commands.ContainsKey(key))
            {
                _output.Log($"[Console] Command '{key}' is already registered.", LogType.Warning);
                return;
            }

            _commands[key] = command;
        }

        public void Execute(string rawInput)
        {
            if (string.IsNullOrWhiteSpace(rawInput)) return;

            var parts = rawInput.Trim().Split(' ');
            var commandName = parts[0].ToLower();
            var args = parts.Length > 1 ? parts[1..] : new string[0];

            if (_commands.TryGetValue(commandName, out var command))
            {
                command.Execute(args, _output);
            }
            else
            {
                _output.Log($"Unknown command: '{commandName}'. Type 'help' for a list.", LogType.Error);
            }
        }

        public IReadOnlyDictionary<string, IConsoleCommand> GetAll() => _commands;
    }
}