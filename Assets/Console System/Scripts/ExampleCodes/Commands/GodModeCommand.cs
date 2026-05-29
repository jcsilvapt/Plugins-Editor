using System.Text;
using jcsilva.ConsoleSystem.Core;

namespace jcsilva.ConsoleSystem.Commands
{
   public class GodModeCommand : IConsoleCommand
    {
        public string Name => "godmode";
        public string Description => "Toggles player invincibility";
        public string Usage => "godmode <on|off>";

        //private readonly PlayerHealth _playerHealth; // your game's player component
        private readonly PlayerHealth _playerHealth;

        public GodModeCommand(PlayerHealth playerHealth) //public GodModeCommand(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        public void Execute(string[] args, IConsoleOutput output)
        {
            if (args.Length == 0)
            {
                output.Log($"Usage: {Usage}", LogType.Warning);
                return;
            }

            switch (args[0].ToLower())
            {
                case "on":
                    _playerHealth.IsInvincible = true;
                    output.Log("God mode enabled.", LogType.Success);
                    break;
                case "off":
                    _playerHealth.IsInvincible = false;
                    output.Log("God mode disabled.", LogType.Success);
                    break;
                default:
                    output.Log($"Invalid argument '{args[0]}'. Usage: {Usage}", LogType.Error);
                    break;
            }
        }
    }
}