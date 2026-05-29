using jcsilva.ConsoleSystem.Core;
using jcsilva.ConsoleSystem.Commands;

public class DebugConsoleOutput : IConsoleOutput
{
    public void Log (string message, LogType type = LogType.Default)
        => UnityEngine.Debug.Log($"[{type}] {message}");

    public void Clear()
        => UnityEngine.Debug.Log("[Console] Cleared.");
}
