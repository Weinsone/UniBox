﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBehavior : IBotBehavior
{
    public IBot Root { get; set; }
    public AIManager Ai { get; set; }

    public FollowerBehavior(IBot Root) {
        this.Root = Root;
        Ai = new AIManager();
    }

    public void Checkup() {
        if (Ai.IsEnemyInView(Root.EntityGameObject.transform.position, Root.DirectionOfView, Root.ViewAngle, Root.ViewDistance)) {
            OnTargetFound();
        } else {
            DailyRoutine();
        }
    }

    private void DailyRoutine() {
        Root.Controller.Jump();
    }

    private void OnTargetFound() {
        if (Vector3.Distance(Root.EntityGameObject.transform.position, Ai.TargetPosition) > 3) {
            Root.Controller.Goto(Ai.TargetPosition, false);
            Debug.Log("Вижу");
        }
    }

    private void OnTargetLost() {
        Debug.Log("НЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕТ");
    }
}
