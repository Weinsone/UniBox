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

    public void RunProgram(Text programName) {
        GameLevel.LocalPlayer.usingComputer.RunProgram(programName.text);
    }
}
