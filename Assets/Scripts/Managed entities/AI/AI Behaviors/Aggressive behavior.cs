﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveBehavior : IBotBehavior
{
    public IBot Root { get; set; } // кажись костыль. Допустим, в случае OnTargetFound нужно издать специфич. звук бота, то как это сделать без этого поля?
    public AIManager Ai { get; set; }

    public AggressiveBehavior(IBot root) {
        Root = root;
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
        Debug.Log("Да где же этот артефакт едрить его в корень");
    }

    private void OnTargetFound() {
        Debug.Log("А вот и маслята");
        Root.Shoot(Ai.TargetPosition);
    }

    private void OnTargetLost() {
        Debug.Log("Опять артефакты искать");
    }
}
