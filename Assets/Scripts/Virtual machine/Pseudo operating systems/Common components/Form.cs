using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Form
{
    public Canvas computerScreen;
    private GameObject visualForm;
    private List<GameObject> formComponents = new List<GameObject>();

    private float position;
    private float sizeX, sizeY;
    private string name;

    public Form(float position, float sizeX = 512, float sizeY = 512, string name = "Form") {
        visualForm = MonoBehaviour.Instantiate((GameObject)Resources.Load("Virtual machine/Forms/Form"));
        
        this.position = position;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.name = name;
    }

    private void Show(Canvas target) {
        Vector3 oldScale = target.transform.localScale;
        target.transform.localScale = new Vector3(1, 1, 1);

        visualForm.transform.position = target.transform.position;
        visualForm.transform.rotation = target.transform.rotation;
        visualForm.transform.SetParent(target.transform);

        target.transform.localScale = oldScale;
    }

    public void AddComponent() {

    }
}
