using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagedEntity
{
    public int Id { get; set; }
    public string Name { get; set; }


    public GameObject EntityGameObject { get; private set; }
    public IController Controller { get; private set; }
    
    public float ViewAngle { get; set; }
    public float ViewDistance { get; set; }
    public Vector3 DirectionOfView {
        get {
            return EntityGameObject.transform.forward;
        }
    }

    public ManagedEntity(string controllerName, ControllerList.Types controllerType) {
        EntityGameObject = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/" + controllerName));
        switch (controllerType) {
            case ControllerList.Types.player:
                Controller = EntityGameObject.AddComponent<PlayerController>();
                break;
            case ControllerList.Types.bot:
                Controller = EntityGameObject.AddComponent<BotController>();
                break;
            default:
                return;
        }
        Controller.ApplySettings(EntityGameObject.GetComponent<EntitySettings>());
    }

    public void Animate(string animationName) {
        Controller.SetAnimation(animationName);
    }

    public void Goto(Vector3 position, bool immediately) {
        Controller.Goto(position, immediately);
    }

    public void SetController(string controllerName) {
        Transform previousModelTransform = EntityGameObject.transform;

        MonoBehaviour.Destroy(EntityGameObject);
        EntityGameObject = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/" + controllerName));

        EntityGameObject.transform.position = previousModelTransform.position;
        EntityGameObject.transform.rotation = previousModelTransform.rotation;
        EntityGameObject.transform.localScale = previousModelTransform.localScale;

        Controller = EntityGameObject.GetComponent<PlayerController>();
    }
}
