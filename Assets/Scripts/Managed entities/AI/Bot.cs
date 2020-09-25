using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Бот обыкновенный
*/
public class Bot : ManagedEntity, IBot
{
    public IBotBehavior Behavior { get; set; }
    public int Reaction { get; set; }
    public int Remembrance { get; set; }

    public Bot(int id, string name, BotBehaviorList.Behaviors behavior, ControllerList.Controllers controllerType, Vector3 spawnPosition, float viewAngle = 45f, float viewDistance = 100f) : base(controllerType, spawnPosition) {
        Id = id;
        Name = name;
        Behavior = BotBehaviorList.Assign(this, behavior);
        ViewAngle = viewAngle;
        ViewDistance = viewDistance;
    }

    public void MoveTowards(Vector3 position) {
        controller.MoveTowards(position);
    }

    public bool MoveTo(Vector3 target, float minimalDistance) { // minimalDistance - default value is 0.1f
        if (Vector3.Distance(GetPosition(), target) > minimalDistance) {
            controller.MoveTowards(AIManager.GetDirectionOfPath(GetPosition(), target));
            return false;
        } else {
            controller.Stop();
            return true;
        }
    }

    public void LookAt(Vector3 target) {
        controller.LookAt(target);
    }

    public void MakeSound() {
        
    }

    public void Shoot(Vector3 target) {

    }
}
