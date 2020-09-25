using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveBehavior : IBotBehavior
{
    public IBot Root { get; set; } // кажись костыль. Допустим, в случае OnTargetFound нужно издать специфич. звук бота, то как это сделать без этого поля?
    public Vector3 targetPosition;

    public AggressiveBehavior(IBot root) {
        Root = root;
    }

    public void Checkup() {
        if (AIManager.IsEnemyInView(Root.EntityGameObject.transform.position, Root.DirectionOfView, Root.ViewAngle, Root.ViewDistance, out targetPosition)) {
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
        Root.Shoot(targetPosition);
    }

    private void OnTargetLost() {
        Debug.Log("Опять артефакты искать");
    }
}
