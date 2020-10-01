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

    void Start() {
        // helper = GameObject.Find("Helper");
        // pathHelper = GameObject.Find("Path helper");
    }

    void Update() {
        // if (Input.GetKeyUp(KeyCode.L)) {
        //     string code = @"
        //         using UnityEngine;
        //         using UnityEngine.UI;

        //         public class IDE: IProgram {
        //             public string Name {
        //                 get;
        //             } = ""Crap IDE"";
        //             public string Description {
        //                 get;
        //             } = ""Эта программа позволит создавать приложения для компьютера"";
        //             public Form Form {
        //                 get;
        //                 set;
        //             }

        //             public void Main() {
        //                 Debug.Log(Name + "". "" + Description);
        //                 CreateForm();
        //             }

        //             private void CreateForm() {
        //                 Form = Form.Initialize(
        //                 name: ""Crap IDE"", positionX: 400, positionY: 200, sizeX: 450, sizeY: 300);
        //                 Form.AddComponent(""CodeField"", ComponentType.inputField);
        //                 Form.AddComponent(""ErrorList"", ComponentType.text);
        //                 Form.AddComponent(""CompileButton"", ComponentType.button);

        //                 Form.SetComponentSize(""CodeField"", 440, 250);
        //                 Form.SetComponentSize(""ErrorList"", 275, 30);
        //                 // Form.SetComponentSize(""CompileButton"", 160, 30); // default value

        //                 Form.SetComponentPosition(""CodeField"", 5, 5);
        //                 Form.SetComponentPosition(""ErrorList"", 5, 264);
        //                 Form.SetComponentPosition(""CompileButton"", 285, 264);

        //                 GameObject codeField = Form.GetComponentByName(""CodeField"");
        //                 InputField codeFieldProperties = codeField.GetComponent < InputField > ();
        //                 Text errorList = Form.GetProperties < Text > (""ErrorList"");
        //                 Button compileButton = Form.GetProperties < Button > (""CompileButton"");

        //                 codeField.transform.GetChild(0).GetComponent < Text > ().text = ""Enter your code"";
        //                 codeFieldProperties.lineType = InputField.LineType.MultiLineNewline;
        //                 errorList.text = ""No problems have been detected"";

        //                 compileButton.onClick.AddListener(() =>Compile(codeFieldProperties.text, errorList));
        //             }

        //             private void Compile(string code, Text errorList) {
        //                 string errors;
        //                 if (!PluginEngine.Compile(code, true, out errors)) {
        //                 errorList.text = errors;
        //                 } else {
        //                 errorList.text = ""Скомпилировано"";
        //                 }
        //             }
        //         }
        //     ";
        //     if (!PluginEngine.Compile(code, true, out string errors)) {
        //         Debug.Log(errors);
        //     }
        // }
    }
}
