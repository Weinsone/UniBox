using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagedEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    private GameObject entityGameObject;
    public GameObject EntityGameObject { get { return entityGameObject; } }
    public EntitySettings settings;
    protected IController controller;
    
    public float ViewAngle { get; set; }
    public float ViewDistance { get; set; }
    public Vector3 DirectionOfView {
        get {
            return entityGameObject.transform.forward;
        }
    }

    public ManagedEntity(ControllerList.Controllers controllerType, Vector3 spawnPosition) {
        entityGameObject = ControllerList.AssignGameObject(controllerType, spawnPosition);
        settings = entityGameObject.GetComponent<EntitySettings>();
        controller = ControllerList.AssignController(controllerType, ref entityGameObject, settings);
    }

    public void PlayAnimation(string animationName) {
        controller.PlayAnimation(animationName);
    }

    public void Teleport(Vector3 position) {
        if (controller != null) {
            controller.Teleport(position);
        } else {
            entityGameObject.transform.position = position;
        }
    }

    public Vector3 GetPosition() {
        return EntityGameObject.transform.position;
    }

    public void SetController(ControllerList.Controllers controllerType) {
        Transform previousModelTransform = entityGameObject.transform;

        MonoBehaviour.Destroy(entityGameObject);
        entityGameObject = ControllerList.AssignGameObject(controllerType, previousModelTransform.position);
        settings = entityGameObject.GetComponent<EntitySettings>();

        // entityGameObject.transform.position = previousModelTransform.position;
        entityGameObject.transform.rotation = previousModelTransform.rotation;
        entityGameObject.transform.localScale = previousModelTransform.localScale;

        controller = ControllerList.AssignController(controllerType, ref entityGameObject, settings);
    }
}
