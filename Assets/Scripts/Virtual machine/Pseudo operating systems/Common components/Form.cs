using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ComponentType
{
    button, // +
    text, // +
    toggle, // +
    inputField, // +
    scrollView, // +
    audio, // -
    video, // -
    browser // -
}

public class Form : MonoBehaviour
{
    private bool isFormMoving;
    private RectTransform formRect;
    private Dictionary<string, GameObject> components = new Dictionary<string, GameObject>();

    public Canvas targetCanvas;

    // public string formName;
    // public float positionX, positionY;
    // public float sizeX, sizeY;

    // public Form(Canvas target, string name = "Form", float positionX = 0, float positionY = 0, float sizeX = 200, float sizeY = 170) {
    //     computerScreen = target;

    //     visualForm = MonoBehaviour.Instantiate((GameObject)Resources.Load("Virtual machine/Forms/Form"));
    //     visualForm.transform.SetParent(computerScreen.transform);
    //     formRect = visualForm.GetComponent<RectTransform>();

    //     visualForm.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = name;
    //     SetFormPosition(positionX, positionY);
    //     SetFormSize(sizeX, sizeY);
    // }

    private Form() {
        // ТЫ ДАЖЕ НЕ ГРАЖДАНИН!
    }

    // Крутой костыль. И все из-за того что MonoBehaviour почему-то нельзя создавать через конструктор ._.
    public static Form Initialize(string name = "Form", float positionX = 0, float positionY = 0, float sizeX = 200, float sizeY = 170) {
        Form form = Instantiate((GameObject)Resources.Load("UI/Forms/Form")).GetComponent<Form>();
        
        if (GameLevel.LocalPlayer.isUseComputer) {
            form.targetCanvas = GameLevel.LocalPlayer.usingComputer.computerCanvas;
            AdaptiveAndShow(form.targetCanvas, form.transform, form.targetCanvas.transform, 1);
        } else {
            form.targetCanvas = PlayerMenu.canvas;
            AdaptiveAndShow(form.targetCanvas, form.transform, form.targetCanvas.transform, 0);
        }
        form.formRect = form.GetComponent<RectTransform>();

        form.SetFormName(name);
        form.SetFormPosition(positionX, positionY);
        form.SetFormSize(sizeX, sizeY);
        
        return form;
    }

    public void OnFormHeadClick() {
        if (isFormMoving) {
            isFormMoving = false;
        } else {
            Vector3 offset;
            if (targetCanvas.renderMode == RenderMode.ScreenSpaceOverlay) {
                offset = formRect.position - Input.mousePosition;
            } else {
                offset = formRect.position - Raycast.GetHit(Camera.main.ScreenPointToRay(Input.mousePosition)).point;
            }

            isFormMoving = true;
            StartCoroutine("FormMover", offset);
        }
    }

    private IEnumerator FormMover(Vector3 offset) {
        while (isFormMoving) {
            if (targetCanvas.renderMode == RenderMode.WorldSpace) {
                formRect.position = Raycast.GetHit(Camera.main.ScreenPointToRay(Input.mousePosition)).point + offset; // TODO: брать курсор из InputHandler
            } else {
                formRect.position = Input.mousePosition + offset;
            }
            yield return null;
        }
    }

    public void SetFormName(string formName) {
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = formName;
    }

    public void SetFormPosition(float positionX, float positionY) {
        formRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, positionX, formRect.rect.width);
        formRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, positionY, formRect.rect.height);
    }

    public void SetFormSize(float sizeX, float sizeY) {
        RectTransform formContentRect = transform.GetChild(1).GetComponent<RectTransform>();
        formRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, formRect.anchoredPosition.x - formRect.sizeDelta.x / 2, sizeX);

        formContentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, sizeX);
        formContentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0 + formRect.sizeDelta.y, sizeY);
    }

    public static void AdaptiveAndShow(Canvas targetCanvas, Transform component, Transform parent, int childNumber) {
        Vector3 oldScale = targetCanvas.transform.localScale;
        targetCanvas.transform.localScale = new Vector3(1, 1, 1);

        component.position = targetCanvas.transform.position;
        component.rotation = targetCanvas.transform.rotation;
        component.SetParent(childNumber == 0 ? parent.transform : parent.GetChild(childNumber - 1)); // В дереве первый элемент считается нулевым

        targetCanvas.transform.localScale = oldScale;
    }

    public GameObject GetFormComponent(string componentName) {
        foreach (var component in components) {
            if (componentName == component.Key) {
                return component.Value;
            }
        }
        return default;
    }

    public void AddComponent(string componentName, ComponentType componentType, float positionX = default, float positionY = default, float sizeX = default, float sixeY = default) {
        GameObject component = Instantiate((GameObject)Resources.Load("UI/Forms/Components/" + componentType.ToString()));
        AdaptiveAndShow(targetCanvas, component.transform, transform, 2);
        SetComponentPosition(component, positionX, positionY);
        if (sizeX != default || sixeY != default) {
            SetComponentSize(component, sizeX, sixeY);
        }
        components.Add(componentName, component);
        // return component;
    }

    // public void SetComponentValue<TValue>(string componentName, TValue value) {
    //     foreach (var component in components) {
    //         if (componentName == component.Key) {
                
    //         }
    //     }
    // }

    public TProperties GetComponentProperties<TProperties>(string componentName) {
        foreach (var component in components) {
            if (componentName == component.Key) {
                return component.Value.GetComponent<TProperties>();
            }
        }
        return default(TProperties);
    }

    public void SetComponentPosition(GameObject componentObject, float positionX, float positionY) {
        RectTransform componentRect = componentObject.GetComponent<RectTransform>();
        componentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, positionX, componentRect.rect.width);
        componentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, positionY, componentRect.rect.height);
    }

    public void SetComponentPosition(string componentName, float positionX, float positionY) {
        foreach (var component in components) {
            if (component.Key == componentName) {
                SetComponentPosition(component.Value, positionX, positionY);
                return;
            }
        }
        Debug.LogError("Component pos: " + componentName + " не найден");
    }

    public void SetComponentSize(GameObject componentObject, float sizeX, float sizeY) {
        RectTransform componentRect = componentObject.GetComponent<RectTransform>();
        if (sizeX != default) {
            componentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, componentRect.anchoredPosition.x - componentRect.sizeDelta.x / 2, sizeX);
        }
        if (sizeY != default) {
            componentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -componentRect.anchoredPosition.y - componentRect.sizeDelta.y / 2, sizeY);
        }
    }

    public void SetComponentSize(string componentName, float sizeX, float sizeY) {
        foreach (var component in components) {
            if (component.Key == componentName) {
                SetComponentSize(component.Value, sizeX, sizeY);
                return;
            }
        }
        Debug.LogError("Component siz: " + componentName + " не найден");
    }

    public void OnClose(GameObject form) {
        // *пафосная анимация*
        Destroy(form);
    }

    public void OnMaximize() {

    }

    public void OnMinimize() {

    }

    private IEnumerable PositionAnimation((float toX, float toY) parameters) {
        return null;
    }

    private IEnumerator SizeAnimation((float toX, float toY) parameters) {
        return null;
    }
}
