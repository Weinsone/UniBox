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

public class Form
{
    public Canvas computerScreen;
    private GameObject visualForm;
    private Dictionary<string, GameObject> components = new Dictionary<string, GameObject>();

    private float positionX, positionY;
    private float sizeX, sizeY;
    private string name;

    public Form(Canvas target, string name = "Form", float positionX = 0, float positionY = 0, float sizeX = 512, float sizeY = 512) {
        computerScreen = target;
        
        this.name = name;
        this.positionX = positionX;
        this.positionY = positionY;
        this.sizeX = sizeX;
        this.sizeY = sizeY;

        visualForm = MonoBehaviour.Instantiate((GameObject)Resources.Load("Virtual machine/Forms/Form"));
        components.Add(name, visualForm);
        visualForm.transform.SetParent(computerScreen.transform);

        RectTransform formRect = visualForm.GetComponent<RectTransform>();
        formRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, positionX, formRect.rect.width);
        formRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, positionY, formRect.rect.height);

        visualForm.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = name;
    }

    public void Show() { // Устарело
        Vector3 oldScale = computerScreen.transform.localScale;
        computerScreen.transform.localScale = new Vector3(1, 1, 1);

        visualForm.transform.position = computerScreen.transform.position;
        visualForm.transform.rotation = computerScreen.transform.rotation;
        visualForm.transform.SetParent(computerScreen.transform);

        computerScreen.transform.localScale = oldScale;
    }

    public void AddComponent(string name, ComponentType componentType, float positionX = 0, float positionY = 0) {
        GameObject component = MonoBehaviour.Instantiate((GameObject)Resources.Load("Virtual machine/Forms/Components/" + componentType.ToString()));
        component.transform.SetParent(visualForm.transform.GetChild(1));

        RectTransform componentRect = component.GetComponent<RectTransform>();
        componentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, positionX, componentRect.rect.width);
        componentRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, positionY, componentRect.rect.height);
        
        components.Add(name, component);
    }

    public T GetComponentByName<T>(string componentName) {
        foreach(var component in components) {
            if (componentName == component.Key) {
                return component.Value.GetComponent<T>();
            }
        }
        return default(T);
    }

    public void SetSize(string componentName, float sizeX, float sizeY) {

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
