using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormButtonHandler : MonoBehaviour // TODO: все прямые вызовы PluginEngine заменить на Server.ComplileProgram и т.д
{
    private bool isFormMoving;

    public void OpenStartMenu(GameObject startMenu) {
        // GameObject startMenu = this.transform.GetChild(1).gameObject;
        startMenu.SetActive(!startMenu.activeSelf);
    }

    public void OnFormHeadClick(GameObject form) {
        if (isFormMoving) {
            isFormMoving = false;
        } else {
            RectTransform formRect = form.GetComponent<RectTransform>();
            Vector3 offset = formRect.position - Input.mousePosition;

            isFormMoving = true;
            StartCoroutine("FormMover", new Tuple<RectTransform, Vector3>(formRect, offset));
        }
    }

    IEnumerator FormMover(Tuple<RectTransform, Vector3> parameters) {
        while(isFormMoving) {
            parameters.Item1.position = Input.mousePosition + parameters.Item2;
            yield return null;
        }
    }

    public void CompileProgram(/*VirtualMachine computer*/ Text code) {
        PluginEngine.Compile(code.text, "IdeTest", true);
        // TODO: Вывод ошибки на форму
    }

    public void RunProgram(Canvas target) {
        // Canvas target = this.transform.parent.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<Canvas>();
        string programName = this.transform.GetChild(0).gameObject.GetComponent<Text>().text;

        IProgram virtualProgram = PluginEngine.GetLibrary<IProgram>(programName);
        if (virtualProgram != null) {
            virtualProgram.Form.computerScreen = target;
            virtualProgram.Form.Show();
        } else {
            Debug.LogError("blyat");
        }
    }
}
