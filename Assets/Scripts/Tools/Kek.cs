using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
    Ядерный полигон испытаний
*/
public class Kek : MonoBehaviour
{
    private Form form;
    public Canvas canvas;
    string errors;

    void Start() {
        // form = Form.Initialize(
        //     target: canvas,
        //     name: "Crap IDE",
        //     positionX: 400,
        //     positionY: 200,
        //     sizeX: 450,
        //     sizeY: 300
        // );
        // form.AddComponent("CodeField", ComponentType.inputField);
        // form.AddComponent("ErrorList", ComponentType.text);
        // form.AddComponent("CompileButton", ComponentType.button);

        // form.SetComponentSize("CodeField", 440, 250);
        // form.SetComponentSize("ErrorList", 275, 30);
        // // test.SetComponentSize("CompileButton", 160, 30); // default value

        // form.SetComponentPosition("CodeField", 5, 5);
        // form.SetComponentPosition("ErrorList", 5, 264);
        // form.SetComponentPosition("CompileButton", 285, 264);

        // GameObject codeField = form.GetFormComponent("CodeField");
        // InputField codeFieldProperties = codeField.GetComponent<InputField>();
        // Text errorList = form.GetComponentProperties<Text>("ErrorList");
        // Button compileButton = form.GetComponentProperties<Button>("CompileButton");

        // codeField.transform.GetChild(0).GetComponent<Text>().text = "Enter your code";
        // codeFieldProperties.lineType = InputField.LineType.MultiLineNewline;
        // errorList.text = "No problems have been detected";
        // compileButton.onClick.AddListener(() => PluginEngine.Compile(codeField.transform.GetChild(2).GetComponent<Text>().text, "IdeTest", true));
    }

    void Haha(string говнокод) {
        string тутМогутбытьОшибки;
        if (PluginEngine.Compile(говнокод, "IDE Test", true, out тутМогутбытьОшибки)) {
            Debug.Log("Красава");
        } else {
            Debug.LogError(тутМогутбытьОшибки);
        }
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.L)) {
            form = Form.Initialize(
                target: canvas,
                name: "Crap IDE",
                positionX: 400,
                positionY: 200,
                sizeX: 450,
                sizeY: 300
            );
            form.AddComponent("CodeField", ComponentType.inputField);
            form.AddComponent("ErrorList", ComponentType.text);
            form.AddComponent("CompileButton", ComponentType.button);

            form.SetComponentSize("CodeField", 440, 250);
            form.SetComponentSize("ErrorList", 275, 30);
            // test.SetComponentSize("CompileButton", 160, 30); // default value

            form.SetComponentPosition("CodeField", 5, 5);
            form.SetComponentPosition("ErrorList", 5, 264);
            form.SetComponentPosition("CompileButton", 285, 264);

            GameObject codeField = form.GetFormComponent("CodeField");
            InputField codeFieldProperties = codeField.GetComponent<InputField>();
            Text errorList = form.GetComponentProperties<Text>("ErrorList");
            Button compileButton = form.GetComponentProperties<Button>("CompileButton");

            codeField.transform.GetChild(0).GetComponent<Text>().text = "Enter your code";
            codeFieldProperties.lineType = InputField.LineType.MultiLineNewline;
            errorList.text = "No problems have been detected";
            compileButton.onClick.AddListener(() => Haha(codeField.transform.GetChild(2).GetComponent<Text>().text));
        }

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
