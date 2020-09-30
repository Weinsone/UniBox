using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
    Ядерный полигон испытаний
*/
public class Kek : MonoBehaviour
{
    public static GameObject helper, pathHelper;
    public IBot bot;

    void Start() {
        // helper = GameObject.Find("Helper");
        helper = GameObject.Find("Cube");
        pathHelper = GameObject.Find("Path helper");
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.K)) {
            string code = @"
                using UnityEngine;
                using UnityEngine.UI;

                public class IDE : IProgram
                {
                    public string Name { get; } = ""Crap IDE"";
                    public string Description { get; } = ""Эта программа позволит создавать приложения для компьютера"";
                    public Form Form { get; set; }

                    public void Main() {
                        Debug.Log(Name + "". "" + Description);
                        CreateForm();
                    }

                    private void CreateForm() {
                        Form = Form.Initialize(
                            name: ""Crap IDE"",
                            positionX: 400,
                            positionY: 200,
                            sizeX: 450,
                            sizeY: 300
                        );
                        Form.AddComponent(""CodeField"", ComponentType.inputField);
                        Form.AddComponent(""ErrorList"", ComponentType.text);
                        Form.AddComponent(""CompileButton"", ComponentType.button);

                        Form.SetComponentSize(""CodeField"", 440, 250);
                        Form.SetComponentSize(""ErrorList"", 275, 30);
                        // Form.SetComponentSize(""CompileButton"", 160, 30); // default value

                        Form.SetComponentPosition(""CodeField"", 5, 5);
                        Form.SetComponentPosition(""ErrorList"", 5, 264);
                        Form.SetComponentPosition(""CompileButton"", 285, 264);

                        GameObject codeField = Form.GetFormComponent(""CodeField"");
                        InputField codeFieldProperties = codeField.GetComponent<InputField>();
                        Text errorList = Form.GetComponentProperties<Text>(""ErrorList"");
                        Button compileButton = Form.GetComponentProperties<Button>(""CompileButton"");

                        codeField.transform.GetChild(0).GetComponent<Text>().text = ""Enter your code"";
                        codeFieldProperties.lineType = InputField.LineType.MultiLineNewline;
                        errorList.text = ""No problems have been detected"";
                        
                        compileButton.onClick.AddListener(() => Compile(codeFieldProperties.text));
                    }

                    private void Compile(string code) {
                        string errorList;
                        if (!PluginEngine.Compile(code, ""IDE Test"", true, out errorList)) {
                            Debug.Log(""IDE: "" + errorList);
                        } else {
                            Debug.Log(""Красава"");
                        }
                    }
                }
            ";
            string errors;
            if (PluginEngine.Compile(code, "Test", true, out errors)) {
                Debug.Log("dada");
            } else {
                Debug.Log("nene: " + errors);
            }
        }
    }
}
