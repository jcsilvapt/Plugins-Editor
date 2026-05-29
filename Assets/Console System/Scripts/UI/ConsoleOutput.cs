using System.Text;
using TMPro;
using UnityEngine;
using jcsilva.ConsoleSystem.Core;
using LogType = jcsilva.ConsoleSystem.Core.LogType;

namespace jcsilva.ConsoleSystem.UI
{
    public class ConsoleOutput : IConsoleOutput
    {
        private readonly TMP_Text _logText;
        private readonly StringBuilder _log = new();

        private const string ColorDefault = "#FFFFFF";
        private const string ColorWarning = "#FFD700";
        private const string ColorError = "#FF4444";
        private const string ColorSuccess = "#44FF88";

        public ConsoleOutput(TMP_Text logText)
        {
            _logText = logText;
        }

        public void Log(string message, LogType type = LogType.Default)
        {
            string color = type switch
            {
                LogType.Warning => ColorWarning,
                LogType.Error   => ColorError,
                LogType.Success => ColorSuccess,
                _ => ColorDefault
            };

            _log.AppendLine($"<color={color}>{message}</color>");
            _logText.text = _log.ToString();
        }

        public void Clear()
        {
            _log.Clear();
            _logText.text = string.Empty;
        }  
    }
}