using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormButtonHandler : MonoBehaviour // TODO: все прямые вызовы PluginEngine заменить на Server.ComplileProgram и т.д
{
    public void OpenStartMenu(GameObject startMenu) {
        // GameObject startMenu = this.transform.GetChild(1).gameObject;
        startMenu.SetActive(!startMenu.activeSelf);
    }

    public void CompileProgram(/*VirtualMachine computer*/ Text code, Text errorList, out string error) {
        PluginEngine.Compile(code.text, "IdeTest", true, out error);
        errorList.text = error;
    }

    public void RunProgram(Text programName) {
        GameLevel.LocalPlayer.usingComputer.RunProgram(programName.text);
    }
}
