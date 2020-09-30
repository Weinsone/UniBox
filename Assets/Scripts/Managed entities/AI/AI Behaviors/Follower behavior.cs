using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBehavior : IBotBehavior
{
    public IBot Root { get; set; }
    public Vector3 targetPosition;

    public FollowerBehavior(IBot Root) {
        this.Root = Root;
    }

    public void Checkup() {
        // if (AI.IsEnemyInView(Root.EntityGameObject.transform.position, Root.DirectionOfView, Root.ViewAngle, Root.ViewDistance, out targetPosition)) {
        //     OnTargetFound();
        // } else {
        //     DailyRoutine();
        // }
        DailyRoutine();
    }

    private void DailyRoutine() {
        Root.MoveTo(GameLevel.LocalPlayer.GetPosition(), 1.5f);
        Root.LookAt(GameLevel.LocalPlayer.GetPosition());
    }

    private void OnTargetFound() {
        
    }

    private void OnTargetLost() {
        Debug.Log("НЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕТ");
    }
}
