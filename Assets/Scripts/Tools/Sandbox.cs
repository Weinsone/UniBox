using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    Ядерный полигон испытаний
*/
public class Sandbox : MonoBehaviour
{
    public Canvas canvas;

    void Start() {
        Form test = new Form(
            target: canvas,
            name: "Test"
        );
        test.AddComponent("button1", ComponentType.button);
        Button button1 = test.GetComponentByName<Button>("button1");
        
    }

    void Update() {
        

        // if (Input.GetKeyUp(KeyCode.F)) {
        //     // Server.AddBot(new Bot(0, "Classic Emeaya", BotBehaviorList.Behaviors.follower, ControllerList.Controllers.mainPlayer, 60, 100));
        //     string code = @"
        //         public class BotSpawner : IPlugin
        //         {
        //             public string Name { get; } = ""Bot spawner"";
        //             public string Description { get; } = ""This plugin creates bots"";

        //             public void Main() {
        //                 Server.AddBot(new Bot(0, ""Classic Emeaya"", BotBehaviorList.Behaviors.follower, ControllerList.Controllers.mainPlayer, 60, 100));
        //             }
        //         }
        //     ";
        //     PluginEngine.Compile(code, "Test", false);
        // }
        // if (Input.GetKeyUp(KeyCode.R)) {
        //     PluginEngine.RefreshPlugins();
        //     foreach(var plugin in PluginEngine.Plugins) {
        //         plugin.Main();
        //         Debug.Log("Test log");
        //     }
        // }
        // if (Input.GetKeyUp(KeyCode.I)) {
        //     PluginEngine.InteractiveSharp.Execute(@"Server.AddBot(new Bot(0, ""Classic Emeaya"", BotBehaviorList.Behaviors.follower, ControllerList.Controllers.mainPlayer, 60, 100));");
        // }
    }

    // int kek = 0;
    private void FixedUpdate() {
        // if (kek == 10) { // Таймер от бога
            foreach (var bot in Server.Bots) {
                bot.Behavior.Checkup();
            }
        //     kek = 0;
        // } else {
        //     kek++;
        // }
    }
}
