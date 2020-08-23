using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Form
{
    private Canvas computerScreen;
    private GameObject visualForm;
    private List<GameObject> formComponents = new List<GameObject>();

    private float position;
    private float sizeX, sizeY;
    private string name;

    public Form(float position, float sizeX = 512, float sizeY = 512, string name = "Form") {
        computerScreen = GameLevel.usedComputer.screenCanvas;
        visualForm = MonoBehaviour.Instantiate((GameObject)Resources.Load("Forms/Form"));
        
        this.position = position;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.name = name;
    }

    private void View() {
        // computerScreen + visualForm
    }

    public void AddComponent() {

    }
}
