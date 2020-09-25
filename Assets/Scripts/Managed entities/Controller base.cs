using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    // float Speed { get; set; }
    Vector3 EyeLevel { get; set; }
    Vector3 Velocity { get; }
    void ApplySettings(EntitySettings settings);
    void PlayAnimation(string animationName);
    // void SetSpeed()
    void MoveTowards(Vector3 position);
    void Teleport(Vector3 position);
    void LookAt(Vector3 target);
    void Look(Quaternion direction);
    void Stop(); // для плавного убавления скорости
    void Jump();
}

public static class ControllerList
{
    public enum Controllers
    {
        assistant
    }

    public static GameObject AssignGameObject(Controllers controller, Vector3 spawnPosition) {
        switch (controller) {
            case Controllers.assistant:
                return MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/Assistant"), spawnPosition, Quaternion.Euler(0, 0, 0));
            default:
                return null;
        }
    }

    public static IController AssignController(Controllers controllerType, ref GameObject target, EntitySettings settings) {
        IController controller;
        switch (controllerType) {
            case Controllers.assistant:
                controller = target.AddComponent<HumanoidController>();
                controller.ApplySettings(settings);
                return controller;
            default:
                return null;
        }
    }
}
