using UnityEngine;

namespace jcsilva.ConsoleSystem.Core {
    public interface IConsoleCommand
    {
        string Name { get; }
        string Description { get; }
        string Usage { get; }
        void Execute(string[] args, IConsoleOutput output);
    }
}