using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormButtonHandler : MonoBehaviour // TODO: все прямые вызовы PluginEngine заменить на Server.ComplileProgram и т.д
{
    public void OpenStartMenu() {
        GameObject startMenu = this.transform.GetChild(1).gameObject;
        startMenu.SetActive(!startMenu.activeSelf);
    }

    public void CompileProgram(VirtualMachine from, string code, string programName) {
        PluginEngine.Compile(code, programName, true);
    }

    public void RunProgram() {
        Canvas target = this.transform.parent.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<Canvas>();
        string programName = this.transform.GetChild(0).gameObject.GetComponent<Text>().text;

        IProgram virtualProgram = PluginEngine.GetProgram(programName);
        virtualProgram.Form.computerScreen = target;
    }
}
