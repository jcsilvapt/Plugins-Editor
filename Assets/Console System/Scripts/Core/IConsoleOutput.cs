using UnityEngine;

namespace jcsilva.ConsoleSystem.Core
{
    public enum LogType { Default, Warning, Error, Success }
    public interface IConsoleOutput
    {
        void Log(string message, LogType type = LogType.Default);
        void Clear();
    }
}