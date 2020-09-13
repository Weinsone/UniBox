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

    void Start() {
        
    }

    void Update() {
        // if (Input.GetKeyUp(KeyCode.L)) {
        //     form = Form.Initialize(
        //         name: "Crap IDE",
        //         positionX: 400,
        //         positionY: 200,
        //         sizeX: 450,
        //         sizeY: 300
        //     );
        //     form.AddComponent("CodeField", ComponentType.inputField);
        //     form.AddComponent("ErrorList", ComponentType.text);
        //     form.AddComponent("CompileButton", ComponentType.button);

        //     form.SetComponentSize("CodeField", 440, 250);
        //     form.SetComponentSize("ErrorList", 275, 30);
        //     // test.SetComponentSize("CompileButton", 160, 30); // default value

        //     form.SetComponentPosition("CodeField", 5, 5);
        //     form.SetComponentPosition("ErrorList", 5, 264);
        //     form.SetComponentPosition("CompileButton", 285, 264);

        //     GameObject codeField = form.GetFormComponent("CodeField");
        //     InputField codeFieldProperties = codeField.GetComponent<InputField>();
        //     Text errorList = form.GetComponentProperties<Text>("ErrorList");
        //     Button compileButton = form.GetComponentProperties<Button>("CompileButton");

        //     codeField.transform.GetChild(0).GetComponent<Text>().text = "Enter your code";
        //     codeFieldProperties.lineType = InputField.LineType.MultiLineNewline;
        //     errorList.text = "No problems have been detected";
            
        //     compileButton.onClick.AddListener(() => {
        //         string error;
        //         if (PluginEngine.Compile(codeField.transform.GetChild(2).GetComponent<Text>().text, "IDE Test", true, out error)) {
        //             errorList.text = "Красава";
        //             GameLevel.LocalPlayer.usingComputer.UpdateProgramList();
        //         } else {
        //             errorList.text = error;
        //         }
        //     });
        // }
        // if (Input.GetKeyUp(KeyCode.R)) {
        //     string errorList;
        //     string code = @"
        //         using UnityEngine;
        //         using UnityEngine.UI;

        //         public class IDE : IProgram
        //         {
        //             public string Name { get; } = ""Crap IDE"";
        //             public string Description { get; } = ""Эта программа позволит создавать приложения для компьютера"";
        //             public Form Form { get; set; }

        //             public void Main() {
        //                 Debug.Log(Name + "". "" + Description);
        //                 CreateForm();
        //             }

        //             private void CreateForm() {
        //                 Form = Form.Initialize(
        //                     name: ""Crap IDE"",
        //                     positionX: 400,
        //                     positionY: 200,
        //                     sizeX: 450,
        //                     sizeY: 300
        //                 );
        //                 Form.AddComponent(""CodeField"", ComponentType.inputField);
        //                 Form.AddComponent(""ErrorList"", ComponentType.text);
        //                 Form.AddComponent(""CompileButton"", ComponentType.button);

        //                 Form.SetComponentSize(""CodeField"", 440, 250);
        //                 Form.SetComponentSize(""ErrorList"", 275, 30);
        //                 // Form.SetComponentSize(""CompileButton"", 160, 30); // default value

        //                 Form.SetComponentPosition(""CodeField"", 5, 5);
        //                 Form.SetComponentPosition(""ErrorList"", 5, 264);
        //                 Form.SetComponentPosition(""CompileButton"", 285, 264);

        //                 GameObject codeField = Form.GetFormComponent(""CodeField"");
        //                 InputField codeFieldProperties = codeField.GetComponent<InputField>();
        //                 Text errorList = Form.GetComponentProperties<Text>(""ErrorList"");
        //                 Button compileButton = Form.GetComponentProperties<Button>(""CompileButton"");

        //                 codeField.transform.GetChild(0).GetComponent<Text>().text = ""Enter your code"";
        //                 codeFieldProperties.lineType = InputField.LineType.MultiLineNewline;
        //                 errorList.text = ""No problems have been detected"";
                        
        //                 compileButton.onClick.AddListener(() => Compile(codeFieldProperties.text));
        //             }

        //             private void Compile(string code) {
        //                 string errorList;
        //                 if (!PluginEngine.Compile(code, ""IDE Test"", true, out errorList)) {
        //                     Debug.Log(""IDE: "" + errorList);
        //                 } else {
        //                     Debug.Log(""Красава"");
        //                 }
        //             }
        //         }
        //         ";
        //     if (!PluginEngine.Compile(code, "IDE", true, out errorList)) {
        //         Debug.LogError("Sandbox: " + errorList);
        //     }
        //     GameLevel.LocalPlayer.usingComputer.UpdateProgramList();
        // }
        // if (Input.GetKeyUp(KeyCode.F)) {
        //     GameLevel.LocalPlayer.usingComputer.installedPrograms[0].Main();
        // }

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
    // void FixedUpdate() {
    //     if (kek == 10) { // Таймер от бога
    //         foreach (var bot in Server.Bots) {
    //             bot.Behavior.Checkup();
    //         }
    //         kek = 0;
    //     } else {
    //         kek++;
    //     }
    // }
}
