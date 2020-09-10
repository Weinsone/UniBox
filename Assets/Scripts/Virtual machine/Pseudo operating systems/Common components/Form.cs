using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ComponentType
{
    button,
    edit,
    scrollBox,
    audio,
    video,
    browser
}

public class Form : MonoBehaviour
{
    private bool isFormMoving;
    private RectTransform formRect;
    private Dictionary<string, GameObject> components = new Dictionary<string, GameObject>();

    public Canvas computerScreen;

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
    public static Form Initialize(Canvas target, string name = "Form", float positionX = 0, float positionY = 0, float sizeX = 200, float sizeY = 170) {
        Form form = Instantiate((GameObject)Resources.Load("Virtual machine/Forms/Form")).GetComponent<Form>();
        
        form.computerScreen = target;

        form.transform.SetParent(form.computerScreen.transform);
        form.formRect = form.GetComponent<RectTransform>();

        form.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = name;
        form.SetFormPosition(positionX, positionY);
        form.SetFormSize(sizeX, sizeY);
        
        return form;
    }

    public void OnFormHeadClick() {
        if (isFormMoving) {
            isFormMoving = false;
        } else {
            Vector3 offset = formRect.position - Input.mousePosition;

            isFormMoving = true;
            StartCoroutine("FormMover", offset);
        }
    }

    private IEnumerator FormMover(Vector3 offset) {
        while(isFormMoving) {
            // SetFormPosition(Input.mousePosition.x + offset.x, Input.mousePosition.y + offset.y); // TODO: брать курсор из InputHandler
            formRect.position = Input.mousePosition + offset;
            yield return null;
        }
    }

    public void SetFormPosition(float positionX, float positionY) {
        formRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, positionX, formRect.rect.width);
        formRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, positionY, formRect.rect.height);
    }

    public void SetFormSize(float sizeX, float sizeY) {
        RectTransform formContentRect = transform.GetChild(1).GetComponent<RectTransform>();
        formRect.sizeDelta = new Vector2(sizeX, formRect.sizeDelta.y);
        // formRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, sizeX);
        formContentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, sizeX);
        formContentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0 + formRect.sizeDelta.y, sizeY);
    }

    public void Show() { // Устарело
        Vector3 oldScale = computerScreen.transform.localScale;
        computerScreen.transform.localScale = new Vector3(1, 1, 1);

        transform.position = computerScreen.transform.position;
        transform.rotation = computerScreen.transform.rotation;
        transform.SetParent(computerScreen.transform);

        computerScreen.transform.localScale = oldScale;
    }

    public void AddComponent(string name, ComponentType componentType, float positionX = 0, float positionY = 0) {
        GameObject component = Instantiate((GameObject)Resources.Load("Virtual machine/Forms/Components/" + componentType.ToString()));
        component.transform.SetParent(transform.GetChild(1));

        RectTransform componentRect = component.GetComponent<RectTransform>();
        componentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, positionX, componentRect.rect.width);
        componentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, positionY, componentRect.rect.height);
        
        components.Add(name, component);
    }

    public T GetComponent<T>(string componentName) {
        foreach(var component in components) {
            if (componentName == component.Key) {
                return component.Value.GetComponent<T>();
            }
        }
        return default(T);
    }

    public void SetComponentPosition(string componentName, float positionX, float positionY) {

    }

    public void SetComponentSize(string componentName, float sizeX, float sizeY) {

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

    // public class Component
    // {
    //     private GameObject perfab;

    //     public string name;
    //     public FormComponents componentType;
    //     public float position;
    //     public float sizeX, sizeY;

    //     public Component() {

    //     }

    //     public static implicit operator Button(Component component) {
    //         return component.perfab.GetComponent<Button>();
    //     }
    // }
}
