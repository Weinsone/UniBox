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
        if (Ai.IsEnemyInView(Root.EntityModel.transform.position, Root.DirectionOfView, Root.ViewAngle, Root.ViewDistance)) {
            OnTargetFound();
        } else {
            DailyRoutine();
        }
    }

    private void DailyRoutine() {
        Root.Controller.Jump();
    }

    private void OnTargetFound() {
        Vector3 targetDirection = Ai.TargetPosition - Root.EntityModel.transform.position;
        Root.EntityModel.transform.LookAt(Ai.TargetPosition);

        Debug.DrawLine(Root.EntityModel.transform.position, targetDirection, Color.white);

        if (Vector3.Distance(Root.EntityModel.transform.position, Ai.TargetPosition) > 3) {
            Root.EntityModel.transform.position += targetDirection.normalized * 5f * Time.deltaTime;
        }
    }

    private void OnTargetLost() {
        Debug.Log("НЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕТ");
    }
}