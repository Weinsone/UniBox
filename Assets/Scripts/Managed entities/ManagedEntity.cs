using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagedEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GameObject EntityModel { get; private set; }
    public Controller Controller { get; private set; }
    public AnimationManager Animator { get; private set; }
    
    public float ViewAngle { get; set; }
    public float ViewDistance { get; set; }
    public Vector3 DirectionOfView {
        get {
            return EntityModel.transform.forward;
        }
    }

    public ManagedEntity(string controllerName) {
        EntityModel = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/" + controllerName));
        Controller = EntityModel.GetComponent<Controller>();
        Animator = new AnimationManager(Controller, EntityModel.GetComponent<Animator>());
    }

    public void SetController(string controllerName) {
        Transform oldModelTransform = EntityModel.transform;

        MonoBehaviour.Destroy(EntityModel);
        EntityModel = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/" + controllerName));
        Controller = EntityModel.GetComponent<Controller>();

        EntityModel.transform.position = oldModelTransform.position;
        EntityModel.transform.rotation = oldModelTransform.rotation;
    }

    public void GoTo(Vector3 position, bool immediately) {
        if (immediately) {
            EntityModel.transform.position = position;
        } else {
            // тут уже нужно с сеткой навигации играться
        }
    }
}
