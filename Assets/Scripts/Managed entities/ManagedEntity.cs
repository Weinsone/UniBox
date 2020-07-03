using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagedEntity
{
    public GameObject EntityModel { get; private set; }
    public Controller Controller { get; private set; }

    public ManagedEntity(string controllerName) {
        EntityModel = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/" + controllerName));
        Controller = EntityModel.GetComponent<Controller>();
    }

    public void SetController(string controllerName) {
        MonoBehaviour.Destroy(EntityModel);
        EntityModel = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/" + controllerName));
        Controller = EntityModel.GetComponent<Controller>();
    }
}
