using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Бот обыкновенный
*/
public class Bot : ManagedEntity, IBot
{
    public IBotBehavior Behavior { get; set; }
    public int Speed { get; set; }
    public int Remembrance { get; set; }

    public Bot(int id, string name, BotBehaviorList.Behaviors behavior, ControllerList.Controllers controllerName, float viewAngle, float viewDistance) : base(ControllerList.Assign(controllerName), ControllerList.Types.bot) {
        Id = id;
        Name = name;
        Behavior = BotBehaviorList.Assign(this, behavior);
        ViewAngle = viewAngle;
        ViewDistance = viewDistance;
    }

    public void MakeSound() {
        
    }

    public void Shoot(Vector3 target) {

    }
}
