using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBot
{
    int Id { get; set; }
    string Name { get; set; }
    int Reaction { get; set; }
    int Remembrance { get; set; } // время с секундах, где он будет помнить что с кем-то сражался (не то что сржался, скорее видел). По истечению этого значения пойдет по своим делам (0 - злопамятный и будет вечно помнить)
    
    IBotBehavior Behavior { get; set; }
    GameObject EntityGameObject { get; } // реализация в ManagedEntity
    float ViewAngle { get; set; } // в ManagedEntity
    float ViewDistance { get; set; } // в ManagedEntity
    Vector3 DirectionOfView { get; } // в ManagedEntity
    Vector3 GetPosition(); // в ManagedEntity
    
    void SetController(ControllerList.Controllers controllerType); // в ManagedEntity
    void PlayAnimation(string animationName); // в ManagedEntity
    void LookAt(Vector3 target);
    void MoveTowards(Vector3 direction);
    bool MoveTo(Vector3 target, float minimalDistance = 0.1f);
    void Teleport(Vector3 position);
    void MakeSound(); // пердёж
    void Shoot(Vector3 target);
}

public interface IBotBehavior {
    IBot Root { get; set; } // кажись костыль. Допустим, в случае OnTargetFound нужно издать специфич. звук бота, то как это сделать без этого поля?
    void Checkup(); // Момент простоя (ну там ходить кругами, либо просто тупить в стену с анимацией)
}
