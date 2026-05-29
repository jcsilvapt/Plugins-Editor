using jcsilva.ConsoleSystem.Commands;
using jcsilva.ConsoleSystem.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace jcsilva.ConsoleSystem.UI
{
    public class ConsoleUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject _consolePanel;
        [SerializeField] private TMP_Text _logText;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Settings")]
        [SerializeField] private KeyCode _toggleKey = KeyCode.F1;

        private ConsoleRegistry _registry;
        private ConsoleOutput _output;
        private bool _isOpen;

        private void Awake()
        {
            _output = new ConsoleOutput(_logText);
            _registry = new ConsoleRegistry(_output);

            RegisterBuiltInCommands();

            _consolePanel.SetActive(false);
        }

        private void RegisterBuiltInCommands()
        {
            _registry.Register(new HelpCommand(_registry.GetAll()));
            _registry.Register(new ClearCommand(_output));
            //_registry.Register(new GodModeCommand(playerHealth));

            // Buyers register their own commands here
            // or via a registration hook (we'll add that next)
        }

        private void Update()
        {
            if (Input.GetKeyDown(_toggleKey))
                Toggle();

            // Submit on Enter
            if (_isOpen && Input.GetKeyDown(KeyCode.Return))
                Submit();
        }

        private void Toggle()
        {
            _isOpen = !_isOpen;
            _consolePanel.SetActive(_isOpen);

            if (_isOpen)
            {
                _inputField.text = string.Empty;
                _inputField.ActivateInputField(); // focus immediately
                // Pause game time optionally
            }
            else
            {
                _inputField.DeactivateInputField();
            }
        }

        private void Submit()
        {
            var input = _inputField.text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            // Echo the command back so player sees what they typed
            _output.Log($"> {input}");
            _registry.Execute(input);

            _inputField.text = string.Empty;
            _inputField.ActivateInputField();

            // Scroll to bottom after output
            Canvas.ForceUpdateCanvases();
            _scrollRect.verticalNormalizedPosition = 0f;
        }

        public void RegisterCommand(IConsoleCommand command)
        {
            _registry.Register(command);
        }
    }
}
