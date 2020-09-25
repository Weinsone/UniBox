using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ManagedEntity
{
    public Privileges privileges;

    public Vector3 EyeLevel {
        get {
            return controller.EyeLevel;
        }
    }

    public bool isUseComputer;
    public VirtualMachine usingComputer; // Компьютер, который использует игрок. Необходим для вывода формы, т.к в ее конструкторе не указывается инфа о компе, куда она должна быть выведена

    public Player(int id, string nickname, Privileges privileges, ControllerList.Controllers controllerType, Vector3 spawnPosition) : base(controllerType, spawnPosition) {
        Id = id;
        Name = nickname;
        this.privileges = privileges;
    }

    public void MoveTowards(Vector3 direction) {
        Transform targetCamera = GameLevel.LocalPlayerCamera.Camera.transform;
        targetCamera.eulerAngles = new Vector3(0, targetCamera.eulerAngles.y, 0);
        direction = targetCamera.TransformDirection(direction);

        controller.Look(GameLevel.LocalPlayerCamera.Camera.transform.rotation);
        controller.MoveTowards(direction);
    }

    public void Stop() {
        controller.Stop();
    }

    public void Jump() {
        controller.Jump();
    }
}