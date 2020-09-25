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
        // if (Vector3.Distance(Root.GetPosition(), Kek.pathHelper.transform.position) > 2f) {
        //     Debug.Log("Dist: " + Vector3.Distance(Root.GetPosition(), Kek.pathHelper.transform.position) + "; Dir: " + AI.GetDirectionOfPath(Root.GetPosition(), Kek.pathHelper.transform.position));
        //     Root.Goto(AI.GetDirectionOfPath(Root.GetPosition(), Kek.pathHelper.transform.position), false);
        // }
        Root.MoveTo(Kek.pathHelper.transform.position, 1f);
        Root.LookAt(GameLevel.LocalPlayer.GetPosition());
    }

    private void OnTargetFound() {
        
    }

    private void OnTargetLost() {
        Debug.Log("НЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕТ");
    }
}
