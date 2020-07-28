using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBot
{
    IBotBehavior Behavior { get; set; }
    int Id { get; set; }
    string Name { get; set; }
    int Speed { get; set; }
    int Remembrance { get; set; } // время с секундах, где он будет помнить что с кем-то сражался (не то что сржался, скорее видел). По истечению этого значения пойдет по своим делам (0 - злопамятный и будет вечно помнить)
    GameObject EntityModel { get; } // реализация в ManagedEntity
    Controller Controller { get; } // в ManagedEntity
    float ViewAngle { get; set; } // в ManagedEntity
    float ViewDistance { get; set; } // в ManagedEntity
    Vector3 DirectionOfView { get; } // в ManagedEntity
    /* тут должно быть поле с анимацией */
    void SetController(string controllerName); // в ManagedEntity
    void GoTo(Vector3 position, bool immediately); // в ManagedEntity
    void MakeSound(); // пердёж
    void Shoot(Vector3 target);
}

public interface IBotBehavior {
    IBot Root { get; set; }
    void Checkup(); // Момент простоя (ну там ходить кругами, либо просто тупить в стену с анимацией)
}
