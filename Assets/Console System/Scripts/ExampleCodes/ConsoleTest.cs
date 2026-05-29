using UnityEngine;
using jcsilva.ConsoleSystem.Core;
using jcsilva.ConsoleSystem.Commands;

public class ConsoleTest : MonoBehaviour
{

    public PlayerHealth playerHealth; // Assign this in the Unity Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var output = new DebugConsoleOutput();
        var registry = new ConsoleRegistry(output);

        registry.Register(new GodModeCommand(playerHealth));
        registry.Register(new HelpCommand(registry.GetAll())); // fixed

        registry.Execute("help");            // should show both commands
        registry.Execute("unknowncommand");  // error
        registry.Execute("godmode");         // toggles on
        registry.Execute("godmode on");      // explicitly on
        registry.Execute("godmode");         // toggles off
        registry.Execute("godmode off");     // explicitly off
    }

    // Update is called once per frame
    void Update()
    {

    }
}
